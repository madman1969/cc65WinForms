using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Builders;
using cc65Wrapper.Enumerations;
using cc65Wrapper.Models;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Logging;

namespace cc65Wrapper.Services
{
    /// <summary>
    /// Service responsible for launching a configured CC65 emulator for a given project.
    /// </summary>
    /// <remarks>
    /// This class coordinates validation of inputs, resolution of the correct emulator
    /// executable path for the project's target platform, construction of command-line
    /// arguments via an <see cref="IArgumentBuilder{EmulatorLaunchOptions}"/>, and execution
    /// of the emulator using an <see cref="ICommandExecutor"/>.
    /// </remarks>
    public class Cc65EmulatorLauncher : IEmulatorLauncher
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IArgumentBuilder<EmulatorLaunchOptions> _argumentBuilder;
        private readonly ILogger<Cc65EmulatorLauncher> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cc65EmulatorLauncher"/> class.
        /// </summary>
        /// <param name="commandExecutor">Executes external processes and returns execution results.</param>
        /// <param name="argumentBuilder">Builds the emulator command-line arguments from <see cref="EmulatorLaunchOptions"/>.</param>
        /// <param name="logger">Optional logger. If null, a logger will be created via <see cref="Cc65LoggerFactory"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="commandExecutor"/> or <paramref name="argumentBuilder"/> is null.</exception>
        public Cc65EmulatorLauncher(
            ICommandExecutor commandExecutor,
            IArgumentBuilder<EmulatorLaunchOptions> argumentBuilder,
            ILogger<Cc65EmulatorLauncher> logger = null)
        {
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
            _argumentBuilder = argumentBuilder ?? throw new ArgumentNullException(nameof(argumentBuilder));
            _logger = logger ?? Cc65LoggerFactory.CreateLogger<Cc65EmulatorLauncher>();
        }

        /// <summary>
        /// Launches an emulator process for the specified <see cref="CC65Project"/> using the provided emulator configuration.
        /// </summary>
        /// <param name="project">The project that describes the target platform, working directory, and other metadata required to build the emulator command.</param>
        /// <param name="emulators">A configuration object containing paths to emulator executables for supported platforms.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the emulator process to complete.</param>
        /// <returns>
        /// An <see cref="EmulatorLaunchResult"/> describing whether the launch succeeded, the process exit code,
        /// any captured standard output / standard error, and error messages when applicable.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="project"/> or <paramref name="emulators"/> is null (validation is performed internally).</exception>
        /// <exception cref="ArgumentException">If <see cref="CC65Project.WorkingDirectory"/> is null or empty.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">If <see cref="CC65Project.WorkingDirectory"/> does not exist.</exception>
        /// <remarks>
        /// - Validates inputs before attempting to resolve the emulator path.
        /// - Uses <c>GetEmulatorPath</c> to choose the appropriate emulator executable based on the project's target platform.
        /// - Builds arguments via the injected <see cref="IArgumentBuilder{EmulatorLaunchOptions}"/>.
        /// - Executes the emulator via the injected <see cref="ICommandExecutor"/> and logs progress.
        /// - Any exceptions are caught and converted into an <see cref="EmulatorLaunchResult"/> with Success=false.
        /// </remarks>
        public async Task<EmulatorLaunchResult> LaunchAsync(
            CC65Project project,
            Cc65Emulators emulators,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Validate
                ValidateInputs(project, emulators);

                // Get the emulator path for the platform
                var emulatorPath = GetEmulatorPath(project.TargetPlatform, emulators);

                if (string.IsNullOrWhiteSpace(emulatorPath))
                {
                    var error = $"No emulator configured for platform: {project.TargetPlatform}";
                    _logger.LogEmulatorLaunchFailed(error);
                    return new EmulatorLaunchResult
                    {
                        Success = false,
                        ExitCode = -1,
                        Errors = new[] { error },
                        StandardOutput = string.Empty,
                        StandardError = $"No emulator path found for {project.TargetPlatform}"
                    };
                }

                // Build arguments
                var options = new EmulatorLaunchOptions(project, emulatorPath);
                var arguments = _argumentBuilder.Build(options);
                var argumentString = string.Join(" ", arguments);

                _logger.LogEmulatorStarted(emulatorPath, project.TargetPlatform.ToString());
                _logger.LogEmulatorCommand(emulatorPath, argumentString);

                // Launch emulator
                var result = await _commandExecutor.ExecuteAsync(
                    emulatorPath,
                    arguments,
                    workingDirectory: project.WorkingDirectory,
                    cancellationToken: cancellationToken);

                _logger.LogCommandCompleted(result.ExitCode);

                if (result.ExitCode == 0)
                {
                    _logger.LogEmulatorLaunched(0); // Process ID not available from CliWrap result
                }
                else
                {
                    _logger.LogEmulatorLaunchFailed($"Exit code: {result.ExitCode}");
                }

                return new EmulatorLaunchResult
                {
                    Success = result.ExitCode == 0,
                    ExitCode = result.ExitCode,
                    Errors = Array.Empty<string>(),
                    StandardOutput = result.StandardOutput,
                    StandardError = result.StandardError
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Emulator launch failed with exception");
                return new EmulatorLaunchResult
                {
                    Success = false,
                    ExitCode = -1,
                    Errors = new[] { ex.Message },
                    StandardOutput = string.Empty,
                    StandardError = ex.ToString()
                };
            }
        }

        /// <summary>
        /// Validates the provided project and emulator configuration before launching.
        /// </summary>
        /// <param name="project">The project to validate.</param>
        /// <param name="emulators">The emulator configuration to validate.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="project"/> or <paramref name="emulators"/> is null.</exception>
        /// <exception cref="ArgumentException">If <see cref="CC65Project.WorkingDirectory"/> is null or whitespace.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">If the working directory specified by <see cref="CC65Project.WorkingDirectory"/> does not exist.</exception>
        private void ValidateInputs(CC65Project project, Cc65Emulators emulators)
        {
            if (project == null)
            {
                _logger.LogValidationFailed("Project is null");
                throw new ArgumentNullException(nameof(project));
            }

            if (emulators == null)
            {
                _logger.LogValidationFailed("Emulators configuration is null");
                throw new ArgumentNullException(nameof(emulators));
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
        }

        /// <summary>
        /// Resolves the configured emulator executable path for a given project platform.
        /// </summary>
        /// <param name="platform">The project's target platform (e.g., c64, pet, vic20).</param>
        /// <param name="emulators">The emulator configuration containing paths for supported platforms.</param>
        /// <returns>
        /// The path to the emulator executable for the requested platform, or the C64 path as a fallback.
        /// May be null or empty if no path is configured for that platform.
        /// </returns>
        private string GetEmulatorPath(CC65ProjectTypes platform, Cc65Emulators emulators)
        {
            return platform switch
            {
                CC65ProjectTypes.pet => emulators.PetPath,
                CC65ProjectTypes.c64 => emulators.C64Path,
                CC65ProjectTypes.c128 => emulators.C128Path,
                CC65ProjectTypes.vic20 => emulators.Vic20Path,
                CC65ProjectTypes.plus4 or CC65ProjectTypes.c16 => emulators.Plus4Path,
                _ => emulators.C64Path
            };
        }
    }
}
