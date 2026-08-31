using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Models;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Logging;

namespace cc65Wrapper.Services
{
    /// <summary>
    /// CC65 compiler service implementation
    /// </summary>
    /// <remarks>
    /// This service is responsible for:
    /// - Validating the provided <see cref="CC65Project"/> prior to compilation.
    /// - Building command-line arguments using the configured <see cref="IArgumentBuilder{T}"/>.
    /// - Executing the compiler through <see cref="ICommandExecutor"/>.
    /// - Parsing compiler output via <see cref="IErrorParser"/>.
    /// - Reporting progress through both an <see cref="IProgress{BuildProgressEventArgs}"/>
    ///   and the <see cref="ProgressChanged"/> event.
    ///
    /// The service uses dependency injection for all external concerns (execution, argument
    /// building, error parsing and logging) to keep the class testable and focused on orchestration.
    /// </remarks>
    public class Cc65Compiler : ICompiler
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IArgumentBuilder<CC65Project> _argumentBuilder;
        private readonly IErrorParser _errorParser;
        private readonly ILogger<Cc65Compiler> _logger;

        /// <summary>
        /// The name of the cl65 executable invoked by this service.
        /// </summary>
        private const string CL65 = "cl65";

        /// <summary>
        /// Environment variable name used to pass the target platform to the cl65 toolchain.
        /// The value is set from <see cref="CC65Project.TargetPlatform"/>.
        /// </summary>
        private const string CC65_TARGET = "CC65_TARGET";

        /// <summary>
        /// Raised when the compilation progress changes. Subscribers receive a
        /// <see cref="BuildProgressEventArgs"/> instance describing the current phase,
        /// a human-readable message and a progress percentage (0-100).
        /// </summary>
        public event EventHandler<BuildProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cc65Compiler"/> class.
        /// </summary>
        /// <param name="commandExecutor">Executes external processes (required).</param>
        /// <param name="argumentBuilder">Builds the compiler arguments for a project (required).</param>
        /// <param name="errorParser">Parses compiler stderr into structured error/warning entries (required).</param>
        /// <param name="logger">Optional logger; if null a default logger is created.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="commandExecutor"/>, <paramref name="argumentBuilder"/>
        /// or <paramref name="errorParser"/> is null.
        /// </exception>
        public Cc65Compiler(
            ICommandExecutor commandExecutor,
            IArgumentBuilder<CC65Project> argumentBuilder,
            IErrorParser errorParser,
            ILogger<Cc65Compiler> logger = null)
        {
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
            _argumentBuilder = argumentBuilder ?? throw new ArgumentNullException(nameof(argumentBuilder));
            _errorParser = errorParser ?? throw new ArgumentNullException(nameof(errorParser));
            _logger = logger ?? Cc65LoggerFactory.CreateLogger<Cc65Compiler>();
        }

        /// <summary>
        /// Compiles the supplied <see cref="CC65Project"/> using the CC65 toolchain.
        /// </summary>
        /// <param name="project">The project to compile. Must be non-null and valid (see <see cref="ValidateProject"/>).</param>
        /// <param name="progress">
        /// Optional progress reporter that will receive <see cref="BuildProgressEventArgs"/>
        /// updates as the compilation proceeds.
        /// </param>
        /// <param name="cancellationToken">Token used to cancel the compilation operation.</param>
        /// <returns>
        /// A <see cref="CompilationResult"/> describing whether the compilation succeeded,
        /// exit code, captured stdout/stderr, parsed errors/warnings and the duration.
        /// </returns>
        /// <remarks>
        /// Behavior notes:
        /// - The method logs the invocation and lifecycle of the compilation.
        /// - It sets the environment variable named by <see cref="CC65_TARGET"/> to the
        ///   project's target platform string before invoking <c>cl65</c>.
        /// - Progress is reported at several named phases: Initializing, ValidatingProject,
        ///   BuildingArguments, Compiling, ParsingErrors and Completed.
        /// - If the underlying process returns a non-zero exit code the result is returned
        ///   with Success == false and any parsed errors; exceptions are caught and a
        ///   failed <see cref="CompilationResult"/> is returned with an error message.
        /// </remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="project"/> is null (via validation).</exception>
        /// <exception cref="ArgumentException">If the project has invalid properties such as an empty working directory or missing input files.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">If the project's working directory does not exist.</exception>
        public async Task<CompilationResult> CompileAsync(
            CC65Project project,
            IProgress<BuildProgressEventArgs> progress = null,
            CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Initialize
                _logger.LogCompilationStarted(project.ProjectName ?? "Unnamed", project.TargetPlatform.ToString());
                ReportProgress(progress, "Initializing...", 0, BuildPhase.Initializing);

                // Validate
                ReportProgress(progress, "Validating project...", 10, BuildPhase.ValidatingProject);
                ValidateProject(project);

                // Build arguments
                ReportProgress(progress, "Building arguments...", 20, BuildPhase.BuildingArguments);
                var arguments = _argumentBuilder.Build(project);
                var argumentString = string.Join(" ", arguments);
                _logger.LogCompilerCommand(CL65, argumentString);

                // Compile
                ReportProgress(progress, "Compiling...", 30, BuildPhase.Compiling);

                var environmentVariables = new Dictionary<string, string>
                {
                    [CC65_TARGET] = project.TargetPlatform.ToString()
                };

                var result = await _commandExecutor.ExecuteAsync(
                    CL65,
                    arguments,
                    environmentVariables,
                    project.WorkingDirectory,
                    cancellationToken);

                _logger.LogCommandCompleted(result.ExitCode);

                // Parse errors
                ReportProgress(progress, "Parsing errors...", 80, BuildPhase.ParsingErrors);
                var errors = _errorParser.Parse(result.StandardError);

                // Log individual errors
                foreach (var error in errors)
                {
                    if (error.Type.Equals("Warning", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogCompilationWarning(error.Filename, error.LineNumber, error.Error);
                    }
                    else
                    {
                        _logger.LogCompilationError(error.Filename, error.LineNumber, error.Error);
                    }
                }

                // Complete
                stopwatch.Stop();
                ReportProgress(progress, "Completed", 100, BuildPhase.Completed);

                var success = result.ExitCode == 0;
                if (success)
                {
                    _logger.LogCompilationSucceeded(stopwatch.ElapsedMilliseconds);
                }
                else
                {
                    _logger.LogCompilationFailed(errors.Count, stopwatch.ElapsedMilliseconds);
                }

                return new CompilationResult
                {
                    Success = success,
                    ExitCode = result.ExitCode,
                    Errors = errors.AsReadOnly(),
                    StandardOutput = result.StandardOutput,
                    StandardError = result.StandardError,
                    Duration = stopwatch.Elapsed
                };
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Compilation failed with exception");
                return CompilationResult.Failed($"Compilation failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates the given <see cref="CC65Project"/> instance and throws an exception
        /// for any invalid state.
        /// </summary>
        /// <param name="project">The project to validate.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="project"/> is null.</exception>
        /// <exception cref="ArgumentException">
        /// If the working directory is null/empty or no input files are provided.
        /// </exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">
        /// If the provided working directory does not exist on disk.
        /// </exception>
        private void ValidateProject(CC65Project project)
        {
            if (project == null)
            {
                _logger.LogValidationFailed("Project is null");
                throw new ArgumentNullException(nameof(project));
            }

            if (string.IsNullOrWhiteSpace(project.WorkingDirectory))
            {
                _logger.LogValidationFailed("Working directory cannot be empty");
                throw new ArgumentException("Working directory cannot be empty", nameof(project));
            }

            if (!System.IO.Directory.Exists(project.WorkingDirectory))
            {
                _logger.LogDirectoryNotFound(project.WorkingDirectory);
                throw new System.IO.DirectoryNotFoundException(
                    $"Working directory not found: {project.WorkingDirectory}");
            }

            if (project.InputFiles == null || !project.InputFiles.Any())
            {
                _logger.LogValidationFailed("Project must have at least one input file");
                throw new ArgumentException("Project must have at least one input file", nameof(project));
            }
        }

        /// <summary>
        /// Reports a progress update to both the provided <see cref="IProgress{BuildProgressEventArgs}"/>
        /// instance (if any) and the <see cref="ProgressChanged"/> event subscribers. Also logs the
        /// build phase and message using the configured logger.
        /// </summary>
        /// <param name="progress">Optional progress reporter to receive the <see cref="BuildProgressEventArgs"/>.</param>
        /// <param name="message">Human readable message describing the current state.</param>
        /// <param name="percent">Progress percentage (0-100).</param>
        /// <param name="phase">The logical build phase associated with this progress update.</param>
        private void ReportProgress(
            IProgress<BuildProgressEventArgs> progress,
            string message,
            double percent,
            BuildPhase phase)
        {
            _logger.LogBuildPhase(phase.ToString(), message);
            var args = new BuildProgressEventArgs(message, percent, phase);
            progress?.Report(args);
            ProgressChanged?.Invoke(this, args);
        }
    }
}
