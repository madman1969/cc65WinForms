using cc65Wrapper.Enumerations;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Builders;
using cc65Wrapper.Infrastructure;
using cc65Wrapper.Services;
using CliWrap;
using CliWrap.Buffered;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace cc65Wrapper
{
    /// <summary>
    /// Holds emulator executable paths for supported platforms and provides
    /// legacy helpers to launch a compiled binary in the selected emulator.
    /// </summary>
    /// <remarks>
    /// This class exists primarily for backward compatibility with older code.
    /// Newer code should use an implementation of <c>IEmulatorLauncher</c>
    /// and resolve it via dependency injection.
    /// </remarks>
    public class Cc65Emulators
    {
        #region Fields and properties

        /// <summary>
        /// Gets or sets the path to the C64 emulator executable.
        /// </summary>
        /// <value>
        /// Full filesystem path to the C64 emulator (WinVICE) executable.
        /// May be empty if not configured.
        /// </value>
        public string C64Path { get; set; }

        /// <summary>
        /// Gets or sets the path to the C128 emulator executable.
        /// </summary>
        /// <value>
        /// Full filesystem path to the C128 emulator (WinVICE) executable.
        /// May be empty if not configured.
        /// </value>
        public string C128Path { get; set; }

        /// <summary>
        /// Gets or sets the path to the CBM PET emulator executable.
        /// </summary>
        /// <value>
        /// Full filesystem path to the PET emulator (WinVICE) executable.
        /// May be empty if not configured.
        /// </value>
        public string PetPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the VIC-20 emulator executable.
        /// </summary>
        /// <value>
        /// Full filesystem path to the VIC-20 emulator (WinVICE) executable.
        /// May be empty if not configured.
        /// </value>
        public string Vic20Path { get; set; }

        /// <summary>
        /// Gets or sets the path to the Plus/4 (and C16) emulator executable.
        /// </summary>
        /// <value>
        /// Full filesystem path to the Plus/4 (or C16) emulator (WinVICE) executable.
        /// May be empty if not configured.
        /// </value>
        public string Plus4Path { get; set; }

        #endregion

        #region Class Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="Cc65Emulators"/> with empty paths.
        /// </summary>
        public Cc65Emulators()
        {
            C64Path = string.Empty;
            C128Path = string.Empty;
            PetPath = string.Empty;
            Vic20Path = string.Empty;
            Plus4Path = string.Empty;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Serializes this instance to a JSON string.
        /// </summary>
        /// <returns>A JSON <c>string</c> representing this <see cref="Cc65Emulators"/> instance.</returns>
        public string AsJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Deserializes a JSON string into a new <see cref="Cc65Emulators"/> instance.
        /// </summary>
        /// <param name="Json">A JSON-formatted <c>string</c> to deserialize.</param>
        /// <returns>A populated <see cref="Cc65Emulators"/> instance, or <c>null</c> if deserialization fails.</returns>
        public static Cc65Emulators FromJson(string Json)
        {
            return JsonConvert.DeserializeObject<Cc65Emulators>(Json);
        }

        /// <summary>
        /// Attempts to launch the compiled project binary in the appropriate WinVICE emulator.
        /// </summary>
        /// <param name="project">The <see cref="CC65Project"/> describing the built output and working directory.</param>
        /// <param name="emulators">The <see cref="Cc65Emulators"/> instance containing emulator executable paths.</param>
        /// <param name="cancellationToken">Optional cancellation token used to cancel the launched process.</param>
        /// <returns>
        /// A <see cref="BufferedCommandResult"/> containing the exit code, standard output and error streams
        /// from the emulator process started by this method.
        /// </returns>
        /// <remarks>
        /// - This is a legacy, static helper retained for backward compatibility.
        /// - For new code prefer using an <c>IEmulatorLauncher</c> implementation.
        /// - The method temporarily changes the process current directory to the project's
        ///   working directory and restores it before returning.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="project"/> or <paramref name="emulators"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <c>project.WorkingDirectory</c> is null, empty or whitespace.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when the project's working directory does not exist.</exception>
        public static async Task<BufferedCommandResult> LaunchEmulatorAsync(
            CC65Project project,
            Cc65Emulators emulators,
            System.Threading.CancellationToken cancellationToken = default
        )
        {
            // Validate inputs
            if (project == null)
                throw new System.ArgumentNullException(nameof(project));

            if (emulators == null)
                throw new System.ArgumentNullException(nameof(emulators));

            if (string.IsNullOrWhiteSpace(project.WorkingDirectory))
                throw new System.ArgumentException("Working directory cannot be empty", nameof(project));

            if (!Directory.Exists(project.WorkingDirectory))
                throw new DirectoryNotFoundException($"Working directory not found: {project.WorkingDirectory}");

            BufferedCommandResult result;

            // Take a copy of the current working directory ...
            var originalDir = Directory.GetCurrentDirectory();

            try
            {
                // Switch to project's working directory ...
                Directory.SetCurrentDirectory(project.WorkingDirectory);

                // Build an arguments list from the project settings to pass to the emulator ...
                List<string> argumentList = BuildArgumentsList(project);

                var selectedEmulator = GetSelectedEmulator(project, emulators);

                // Run the configured emulator with arguments and capture output.
                result = await Cli.Wrap(selectedEmulator)
                    .WithArguments(argumentList)
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            finally
            {
                // Always restore the original working directory ...
                Directory.SetCurrentDirectory(originalDir);
            }

            return result;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds the command-line argument list used to instruct the emulator to autostart the built binary.
        /// </summary>
        /// <param name="project">A <see cref="CC65Project"/> instance which lists the binary to run.</param>
        /// <returns>
        /// A list of <see cref="string"/> arguments suitable for passing to the emulator executable.
        /// The current implementation returns an <c>-autostart</c> option followed by the full path to the output file.
        /// </returns>
        private static List<string> BuildArgumentsList(CC65Project project)
        {
            // Add the autostart flag and the full path to the output binary
            var result = new List<string>
            {
                $"-autostart",
                Path.Combine(project.WorkingDirectory, project.OutputFile)
            };

            return result;
        }

        /// <summary>
        /// Retrieves the emulator executable path selected for the project's target platform.
        /// </summary>
        /// <param name="project">A <see cref="CC65Project"/> instance whose <c>TargetPlatform</c> is used to select the emulator.</param>
        /// <param name="emulators">A <see cref="Cc65Emulators"/> instance that maps platforms to executable paths.</param>
        /// <returns>The file path to the appropriate WinVICE emulator for the project's target platform.</returns>
        /// <remarks>
        /// If the project's <c>TargetPlatform</c> is not recognized, the method falls back to the configured C64 emulator path.
        /// </remarks>
        private static string GetSelectedEmulator(CC65Project project, Cc65Emulators emulators)
        {
            return project.TargetPlatform switch
            {
                CC65ProjectTypes.pet => emulators.PetPath,
                CC65ProjectTypes.c64 => emulators.C64Path,
                CC65ProjectTypes.c128 => emulators.C128Path,
                CC65ProjectTypes.vic20 => emulators.Vic20Path,
                CC65ProjectTypes.plus4 or CC65ProjectTypes.c16 => emulators.Plus4Path,
                _ => emulators.C64Path
            };
        }

        #endregion
    }
}
