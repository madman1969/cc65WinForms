using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Models;

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
            IErrorParser errorParser)
        {
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
            _argumentBuilder = argumentBuilder ?? throw new ArgumentNullException(nameof(argumentBuilder));
            _errorParser = errorParser ?? throw new ArgumentNullException(nameof(errorParser));
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
                ReportProgress(progress, "Initializing...", 0, BuildPhase.Initializing);

                // Validate
                ReportProgress(progress, "Validating project...", 10, BuildPhase.ValidatingProject);
                ValidateProject(project);

                // Build arguments
                ReportProgress(progress, "Building arguments...", 20, BuildPhase.BuildingArguments);
                var arguments = _argumentBuilder.Build(project);

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

                // Parse errors
                ReportProgress(progress, "Parsing errors...", 80, BuildPhase.ParsingErrors);
                var errors = _errorParser.Parse(result.StandardError);

                // Complete
                stopwatch.Stop();
                ReportProgress(progress, "Completed", 100, BuildPhase.Completed);

                return new CompilationResult
                {
                    Success = result.ExitCode == 0,
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
                return CompilationResult.Failed($"Compilation failed: {ex.Message}");
            }
        }

        private void ValidateProject(CC65Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (string.IsNullOrWhiteSpace(project.WorkingDirectory))
                throw new ArgumentException("Working directory cannot be empty", nameof(project));

            if (!System.IO.Directory.Exists(project.WorkingDirectory))
                throw new System.IO.DirectoryNotFoundException(
                    $"Working directory not found: {project.WorkingDirectory}");

            if (project.InputFiles == null || !project.InputFiles.Any())
                throw new ArgumentException("Project must have at least one input file", nameof(project));
        }

        private void ReportProgress(
            IProgress<BuildProgressEventArgs> progress,
            string message,
            double percent,
            BuildPhase phase)
        {
            var args = new BuildProgressEventArgs(message, percent, phase);
            progress?.Report(args);
            ProgressChanged?.Invoke(this, args);
        }
    }
}
