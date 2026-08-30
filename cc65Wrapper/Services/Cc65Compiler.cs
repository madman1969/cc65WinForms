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
    public class Cc65Compiler : ICompiler
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IArgumentBuilder<CC65Project> _argumentBuilder;
        private readonly IErrorParser _errorParser;
        private readonly ILogger<Cc65Compiler> _logger;

        private const string CL65 = "cl65";
        private const string CC65_TARGET = "CC65_TARGET";

        /// <summary>
        /// Raised when compilation progress changes
        /// </summary>
        public event EventHandler<BuildProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Initializes a new instance of the Cc65Compiler class
        /// </summary>
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
        /// Compiles a CC65 project
        /// </summary>
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
