using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Builders;
using cc65Wrapper.Enumerations;
using cc65Wrapper.Models;

namespace cc65Wrapper.Services
{
    /// <summary>
    /// CC65 emulator launcher service implementation
    /// </summary>
    public class Cc65EmulatorLauncher : IEmulatorLauncher
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IArgumentBuilder<EmulatorLaunchOptions> _argumentBuilder;

        /// <summary>
        /// Initializes a new instance of the Cc65EmulatorLauncher class
        /// </summary>
        public Cc65EmulatorLauncher(
            ICommandExecutor commandExecutor,
            IArgumentBuilder<EmulatorLaunchOptions> argumentBuilder)
        {
            _commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
            _argumentBuilder = argumentBuilder ?? throw new ArgumentNullException(nameof(argumentBuilder));
        }

        /// <summary>
        /// Launches an emulator for the specified project
        /// </summary>
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
                    return new EmulatorLaunchResult
                    {
                        Success = false,
                        ExitCode = -1,
                        Errors = new[] { $"No emulator configured for platform: {project.TargetPlatform}" },
                        StandardOutput = string.Empty,
                        StandardError = $"No emulator path found for {project.TargetPlatform}"
                    };
                }

                // Build arguments
                var options = new EmulatorLaunchOptions(project, emulatorPath);
                var arguments = _argumentBuilder.Build(options);

                // Launch emulator
                var result = await _commandExecutor.ExecuteAsync(
                    emulatorPath,
                    arguments,
                    workingDirectory: project.WorkingDirectory,
                    cancellationToken: cancellationToken);

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

        private void ValidateInputs(CC65Project project, Cc65Emulators emulators)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (emulators == null)
                throw new ArgumentNullException(nameof(emulators));

            if (string.IsNullOrWhiteSpace(project.WorkingDirectory))
                throw new ArgumentException("Working directory cannot be empty", nameof(project));

            if (!System.IO.Directory.Exists(project.WorkingDirectory))
                throw new System.IO.DirectoryNotFoundException(
                    $"Working directory not found: {project.WorkingDirectory}");
        }

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
