using cc65Wrapper.Enumerations;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Builders;
using cc65Wrapper.Infrastructure;
using cc65Wrapper.Parsers;
using cc65Wrapper.Services;
using CliWrap;
using CliWrap.Buffered;
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
    /// <remarks>
    /// This class provides backward compatibility with the old static API.
    /// For new code, use ICompiler with dependency injection.
    /// </remarks>
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
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>A <c>BufferedCommandResult</c> instance containing the results of the call out to CC65</returns>
        /// <remarks>
        /// This is a legacy method maintained for backward compatibility.
        /// For new code, use ICompiler with dependency injection.
        /// It builds a valid CC65 cmd-line from the project source files and the project compiler setting
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when project is null</exception>
        /// <exception cref="ArgumentException">Thrown when working directory is empty</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when working directory does not exist</exception>
        public static async Task<BufferedCommandResult> CompileAsync(CC65Project project, System.Threading.CancellationToken cancellationToken = default)
        {
            // Validate inputs
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (string.IsNullOrWhiteSpace(project.WorkingDirectory))
                throw new ArgumentException("Working directory cannot be empty", nameof(project));

            if (!Directory.Exists(project.WorkingDirectory))
                throw new DirectoryNotFoundException($"Working directory not found: {project.WorkingDirectory}");

            BufferedCommandResult result;

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
                    .WithEnvironmentVariables(env => env.Set(CC65_TARGET, project.TargetPlatform.ToString()))
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

        #region Public Helper Methods

        /// <summary>
        /// Parses the <c>ExecutionResult</c> from a build command into a list of <c>string</c> instances
        /// </summary>
        /// <returns>A List of <c>string</c> values representing the individual errors</returns>
        /// <remarks>It also de-duplicates the errors</remarks>
        public static List<string> ErrorsAsStringList(BufferedCommandResult executionResult)
        {
            var splitErrors = executionResult.StandardError.Split(
                new string[] { Environment.NewLine, "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );

            var errorsList = new List<string>(splitErrors);
            var dedupedList = errorsList.Distinct().ToList();

            return dedupedList;
        }

        /// <summary>
        /// Parses the <c>ExecutionResult</c> from a cc65 build command and returns a list of structured <see cref="Cc65Error"/> entries.
        /// </summary>
        /// <param name="executionResult">The buffered command result whose <see cref="BufferedCommandResult.StandardError"/> text contains one or more cc65 error lines.</param>
        /// <returns>
        /// A <see cref="List{Cc65Error}"/> containing one entry for each parsed error line.
        /// If the standard error output contains duplicate lines those are removed before parsing.
        /// </returns>
        /// <remarks>
        /// Expected input format (examples):
        /// - <c>file.asm:warning:unused label</c> (3 parts)
        /// - <c>file.asm:123:error:undefined symbol</c> (4 parts)
        /// - <c>file.asm:123:error:message part1:part2</c> (5+ parts)
        ///
        /// The method splits the raw standard-error text on CR/LF variations, removes empty entries,
        /// de-duplicates lines using LINQ <c>Distinct()</c>, then splits each line on colon (<c>':'</c>)
        /// and maps the resulting segments into <see cref="Cc65Error"/> fields:
        /// - When there are 3 segments: filename, type, error (line number set to 0).
        /// - When there are 4 segments: filename, line number, type, error.
        /// - When there are 5 segments: filename, line number, type, error (last two segments concatenated).
        /// - When there are more than 5 segments: segments 2..end are concatenated into the error text.
        ///
        /// Notes:
        /// - Each parsed segment is trimmed.
        /// - The method uses <c>int.Parse</c> to convert the line number segment; invalid integers will cause a <see cref="FormatException"/>.
        /// - If <paramref name="executionResult"/> or its <c>StandardError</c> property is null, a <see cref="NullReferenceException"/> may occur.
        /// </remarks>
        /// <example>
        /// Sample usage:
        /// <code>
        /// var errors = Cc65Build.ErrorsAsErrorList(result);
        /// foreach (var e in errors) Console.WriteLine($"{e.Filename}:{e.LineNumber}:{e.Type}:{e.Error}");
        /// </code>
        /// </example>
        /// <exception cref="FormatException">Thrown when a parsed line number cannot be converted to an integer.</exception>
        public static List<Cc65Error> ErrorsAsErrorList(BufferedCommandResult executionResult)
        {
            var errorList = new List<Cc65Error>();

            // Split by CR/LF ...
            var firstPass = executionResult.StandardError.Split(
                new string[] { Environment.NewLine, "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );

            // De-duplicate the errors ...
            var tmp = new List<string>(firstPass);
            var dedupedList = tmp.Distinct().ToList();

            // For each error ...
            foreach (var error in dedupedList)
            {
                // Split into component parts, i.e. filename, line number, error type, etc ...
                var errorDetails = error.Split(
                    new string[] { ":" },
                    StringSplitOptions.RemoveEmptyEntries
                );

                switch (errorDetails.Length)
                {
                    case 3:
                        errorList.Add(
                            new Cc65Error(
                                Filename: errorDetails[0].Trim(),
                                LineNumber: 0,
                                Type: errorDetails[1].Trim(),
                                Error: errorDetails[2].Trim()
                            )
                        );
                        break;

                    case 4:
                        errorList.Add(
                            new Cc65Error(
                                Filename: errorDetails[0].Trim(),
                                LineNumber: int.Parse(errorDetails[1].Trim()),
                                Type: errorDetails[2].Trim(),
                                Error: errorDetails[3].Trim()
                            )
                        );
                        break;

                    case 5:
                        errorList.Add(
                            new Cc65Error(
                                Filename: errorDetails[0].Trim(),
                                LineNumber: int.Parse(errorDetails[1].Trim()),
                                Type: errorDetails[2].Trim(),
                                Error: $"{errorDetails[3].Trim()}{errorDetails[4]}"
                            )
                        );
                        break;

                    default:
                        var errorText = string.Empty;
                        for (int i = 2; i < errorDetails.Length; i++)
                        {
                            errorText += errorDetails[i].ToString();
                        }
                        errorList.Add(
                            new Cc65Error(
                                Filename: errorDetails[0].Trim(),
                                LineNumber: 0,
                                Type: errorDetails[1].Trim(),
                                Error: errorText
                            )
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
                project.TargetPlatform.ToString()
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
