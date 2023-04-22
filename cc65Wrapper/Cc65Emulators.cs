﻿using CliWrap;
using CliWrap.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace cc65Wrapper
{
    public class Cc65Emulators
    {
        #region Fields and properties

        /// <summary>
        /// Gets or sets the path for the C64 emulator.
        /// </summary>
        /// <value>
        /// The C64 path.
        /// </value>
        public string c64Path { get; set; }

        /// <summary>
        /// Gets or sets the path for the C128 emulator.
        /// </summary>
        /// <value>
        /// The C128 path.
        /// </value>
        public string c128Path { get; set; }

        /// <summary>
        /// Gets or sets the path for the CBM PET emulator.
        /// </summary>
        /// <value>
        /// The pet path.
        /// </value>
        public string petPath { get; set; }

        /// <summary>
        /// Gets or sets the path for the VIC 20 emulator
        /// </summary>
        public string vic20Path { get; set; }

        /// <summary>
        /// Gets or sets the path for the Plus4 emulator
        /// </summary>
        public string plus4Path { get; set; }

        #endregion

        #region Class Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Cc65Emulators"/> class.
        /// </summary>
        public Cc65Emulators()
        {
            c64Path = string.Empty;
            c128Path = string.Empty;
            petPath = string.Empty;
            vic20Path = string.Empty;
            plus4Path = string.Empty;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves JSON representation of a project
        /// </summary>
        /// <returns>A JSON <c>string</c> of the supplied <c>Cc65Emulators</c> instance</returns>
        public string AsJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Converts JSON <c>string</c> into a populated <c>Cc65Emulators</c> instance
        /// </summary>
        /// <param name="Json">A JSOM <c>string</c> representation of a <c>Cc65Emulators</c> instance</param>
        /// <returns>A populated <c>Cc65Emulators</c> instance</returns>
        public static Cc65Emulators FromJson(string Json)
        {
            return JsonConvert.DeserializeObject<Cc65Emulators>(Json);
        }

        /// <summary>
        /// Attempts to launch the associated binary in the appropriate WinVice emulator for the supplied project
        /// </summary>
        /// <param name="project">A <c>Cc65Project</c> instance</param>
        /// <param name="emulators">A <c>Cc65Emulators</c> instance</param>
        /// <returns>An <c>ExecutionResult</c> instance containing the result of the attempt to launch the emulator</returns>
        public static async Task<ExecutionResult> LaunchEmulator(
            Cc65Project project,
            Cc65Emulators emulators
        )
        {
            ExecutionResult result;

            // Take a copy of the current working directory ...
            var originalDir = Directory.GetCurrentDirectory();

            try
            {
                // Switch to projects working directory ...
                Directory.SetCurrentDirectory(project.WorkingDirectory);

                // Build an arguments list from the project settings to pass to CL65 ...
                List<string> argumentList = BuildArgumentsList(project);

                var selectedEmulator = GetSelectedEmulator(project, emulators);

                // Call CL65 with project settings ...
                result = await Cli.Wrap(selectedEmulator)
                    .SetArguments(argumentList)
                    .EnableExitCodeValidation(false)
                    .ExecuteAsync();
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
        /// Builds the cmd-line options required to build the project binary
        /// </summary>
        /// <param name="project">A <c>Cc65Project</c> instance which lists the binary to run</param>
        /// <returns>A list of <c>string</c> arguements to pass to the 'cl65' compiler</returns>
        /// <remarks>The return value is passed as an arguement list to the 'cl65' compiler</remarks>
        private static List<string> BuildArgumentsList(Cc65Project project)
        {
            // Add the target platform ...
            var result = new List<string>();
            result.Add($"-autostart");
            result.Add(Path.Combine(project.WorkingDirectory, project.OutputFile));

            return result;
        }

        /// <summary>
        /// Retrieves the WinVICE emulator file path currently selected for the project
        /// </summary>
        /// <param name="project">A <c>Cc65Project</c> instance</param>
        /// <param name="emulators">A <c>Cc65Emulators</c> instance</param>
        /// <returns>The file path to the appropriate WinVICE emulator for the project</returns>
        private static string GetSelectedEmulator(Cc65Project project, Cc65Emulators emulators)
        {
            var result = string.Empty;

            switch (project.TargetPlatform)
            {
                case "pet":
                    result = emulators.petPath;
                    break;

                case "c64":
                    result = emulators.c64Path;
                    break;

                case "c128":
                    result = emulators.c128Path;
                    break;

                case "vic20":
                    result = emulators.vic20Path;
                    break;

                case "plus4":
                case "c16":
                    result = emulators.plus4Path;
                    break;

                default:
                    result = emulators.c64Path;
                    break;
            }

            return result;
        }

        #endregion
    }
}
