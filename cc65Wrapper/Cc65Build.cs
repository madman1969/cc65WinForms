using CliWrap;
using CliWrap.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cc65Wrapper
{
    /// <summary>
    /// Wrapper class for CC65. Allows building of defined project
    /// </summary>
    public class Cc65Build
    {
        #region Constants

        /// <summary>
        /// Define more readable placeholders for cl65 cmd line options
        /// </summary>
        const string CL65 = "cl65";
        const string CC65_TARGET = "CC65_TARGET";
        const string TARGET_OPTION = "-t";
        const string OUTPUT_FILE_OPTION = "-o";
        const string OPTIMISE_OPTION = "-O";

        #endregion

        #region Public methods

        /// <summary>
        /// Compiles source files associated with the passed <c>Cc65Project</c>instance into an output file using 'cl65'
        /// from the CC65 compiler suite
        /// </summary>
        /// <param name="project">A populated <c>Cc65Project</c> instance</param>
        /// <returns>An <c>ExecutionResult</c> instance containing the results of the call out to CC65</returns>
        /// <remarks>It builds a valid CC65 cmd-line from the project source files and the project compiler setting</remarks>
        public static async Task<ExecutionResult> CompileAsync(CC65Project project)
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

                // Call CL65 with project settings ...
                result = await Cli.Wrap(CL65)
                    .SetEnvironmentVariable(CC65_TARGET, project.TargetPlatform)
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

        #region Public Helper Methods

        /// <summary>
        /// Parses the <c>ExecutionResult</c> from a build command into a list of <c>string</c> instances
        /// </summary>
        /// <returns>A List of <c>string</c> values representing the individual errors</returns>
        /// <remarks>It also de-duplicates the errors</remarks>
        public static List<string> ErrorsAsStringList(ExecutionResult executionResult)
        {
            var splitErrors = executionResult.StandardError.Split(
                new string[] { Environment.NewLine, "\r", "\n" },
                System.StringSplitOptions.RemoveEmptyEntries
            );

            var errorsList = new List<string>(splitErrors);
            var dedupedList = errorsList.Distinct().ToList();

            return dedupedList;
        }

        /// <summary>
        /// Parses the <c>ExecutionResult</c> from a build command into a list of <c>Cc65Error</c> instances
        /// </summary>
        /// <param name="executionResult">The execution result.</param>
        /// <returns>A list of <c>Cc65Error</c> instances parsed from the passed <c>ExecutionResult</c> instance</returns>
        /// <remarks>It also de-duplicates the errors</remarks>
        public static List<Cc65Error> ErrorsAsErrorList(ExecutionResult executionResult)
        {
            var errorList = new List<Cc65Error>();

            var firstPass = executionResult.StandardError.Split(
                new string[] { Environment.NewLine, "\r", "\n" },
                System.StringSplitOptions.RemoveEmptyEntries
            );

            var tmp = new List<string>(firstPass);
            var dedupedList = tmp.Distinct().ToList();

            foreach (var error in dedupedList)
            {
                var errorDetails = error.Split(
                    new string[] { ":" },
                    System.StringSplitOptions.RemoveEmptyEntries
                );

                switch (errorDetails.Length)
                {
                    case 3:
                        errorList.Add(
                            new Cc65Error
                            {
                                Filename = errorDetails[0].Trim(),
                                LineNumber = 0,
                                Type = errorDetails[1].Trim(),
                                Error = errorDetails[2].Trim()
                            }
                        );
                        break;

                    case 4:
                        errorList.Add(
                            new Cc65Error
                            {
                                Filename = errorDetails[0].Trim(),
                                LineNumber = int.Parse(errorDetails[1].Trim()),
                                Type = errorDetails[2].Trim(),
                                Error = errorDetails[3].Trim()
                            }
                        );
                        break;

                    case 5:
                        errorList.Add(
                            new Cc65Error
                            {
                                Filename = errorDetails[0].Trim(),
                                LineNumber = int.Parse(errorDetails[1].Trim()),
                                Type = errorDetails[2].Trim(),
                                Error = $"{errorDetails[3].Trim()}{errorDetails[4]}"
                            }
                        );
                        break;

                    default:
                        var errorText = string.Empty;
                        for (int i = 2; i < errorDetails.Length; i++)
                        {
                            errorText += errorDetails[i].ToString();
                        }
                        errorList.Add(
                            new Cc65Error
                            {
                                Filename = errorDetails[0].Trim(),
                                LineNumber = 0,
                                Type = errorDetails[1].Trim(),
                                Error = errorText
                            }
                        );
                        break;
                }
            }

            return errorList;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds a list of <c>string</c> arguments to pass to 'cl65' from supplied <c>Cc65Project</c> instance
        /// </summary>
        /// <param name="project">A populated Cc65Project instance</param>
        /// <returns>A List of <c>string</c> instances representing the CL65 arguments</returns>
        private static List<string> BuildArgumentsList(CC65Project project)
        {
            // Add the target platform ...
            var result = new List<string>
            {
                // Add target args ...
                TARGET_OPTION,
                project.TargetPlatform
            };

            // Add input files ...
            foreach (var inputFile in project.InputFiles)
            {
                result.Add(inputFile);
            }

            // Add optimise arg, if needed ...
            if (project.OptimiseCode)
            {
                result.Add(OPTIMISE_OPTION);
            }

            // Add output file args ...
            result.Add(OUTPUT_FILE_OPTION);
            result.Add(project.OutputFile);

            return result;
        }

        #endregion
    }
}
