using System.Collections.Generic;
using System.IO;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Builders
{
    /// <summary>
    /// Options for launching an emulator.
    /// Represents the information required to construct command-line arguments
    /// for an emulator invocation.
    /// </summary>
    /// <param name="Project">The <see cref="CC65Project"/> containing at least the <c>WorkingDirectory</c> and <c>OutputFile</c> to autostart.</param>
    /// <param name="EmulatorPath">The full path to the emulator executable. Not validated by this type.</param>
    public record EmulatorLaunchOptions(
        CC65Project Project,
        string EmulatorPath
    );

    /// <summary>
    /// Builds command-line arguments for launching an emulator.
    /// </summary>
    public class EmulatorArgumentBuilder : IArgumentBuilder<EmulatorLaunchOptions>
    {
        /// <summary>
        /// Builds emulator arguments from the provided <paramref name="options"/>.
        /// Returns an argument list that instructs the emulator to autostart the
        /// project's output file (path joined from the project's working directory and output file name).
        /// </summary>
        /// <param name="options">Launch options containing project and emulator path information.</param>
        /// <returns>
        /// A sequence of command-line arguments. Example:
        /// <c>["-autostart", "C:\path\to\working\dir\output.prg"]</c>
        /// </returns>
        public IEnumerable<string> Build(EmulatorLaunchOptions options)
        {
            return new[]
            {
                "-autostart",
                Path.Combine(options.Project.WorkingDirectory, options.Project.OutputFile)
            };
        }
    }
}
