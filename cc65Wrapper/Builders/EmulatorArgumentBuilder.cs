using System.Collections.Generic;
using System.IO;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Builders
{
    /// <summary>
    /// Options for building emulator arguments
    /// </summary>
    public record EmulatorLaunchOptions(
        CC65Project Project,
        string EmulatorPath
    );

    /// <summary>
    /// Builds command-line arguments for launching an emulator
    /// </summary>
    public class EmulatorArgumentBuilder : IArgumentBuilder<EmulatorLaunchOptions>
    {
        /// <summary>
        /// Builds emulator arguments from launch options
        /// </summary>
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
