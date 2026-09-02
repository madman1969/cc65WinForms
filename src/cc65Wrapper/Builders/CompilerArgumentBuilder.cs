using System.Collections.Generic;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Builders
{
    /// <summary>
    /// Builds command-line arguments for the CC65 compiler.
    /// </summary>
    /// <remarks>
    /// The builder assembles arguments in the order expected by the cc65 tool:
    /// <list type="bullet">
    ///   <item><description>Platform specification: <c>-t &lt;TargetPlatform&gt;</c></description></item>
    ///   <item><description>One or more input files (each added as a separate argument)</description></item>
    ///   <item><description>Optional optimisation flag: <c>-O</c> when <see cref="CC65Project.OptimiseCode"/> is true</description></item>
    ///   <item><description>Output file specification: <c>-o &lt;OutputFile&gt;</c></description></item>
    /// </list>
    /// The returned <see cref="IEnumerable{String}"/> is suitable for joining into a single command-line string
    /// or for use with APIs that accept an argument list.
    /// </remarks>
    public class CompilerArgumentBuilder : IArgumentBuilder<CC65Project>
    {
        /// <summary>
        /// Builds compiler arguments from a <see cref="CC65Project"/> instance.
        /// </summary>
        /// <param name="project">The project containing compiler settings and file lists. This must not be <c>null</c>.</param>
        /// <returns>
        /// An ordered sequence of command-line arguments for the cc65 compiler:
        /// <c>-t &lt;TargetPlatform&gt;, &lt;InputFiles&gt;..., [-O], -o &lt;OutputFile&gt;</c>.
        /// </returns>
        /// <remarks>
        /// The method does not validate the existence of files; it only transforms the project's properties
        /// into the argument list expected by cc65. Consumers should validate file paths and handle quoting
        /// if passing the result as a single string to a process launcher.
        /// </remarks>
        public IEnumerable<string> Build(CC65Project project)
        {
            var arguments = new List<string>
            {
                "-t",
                project.TargetPlatform.ToString()
            };

            // Add input files
            arguments.AddRange(project.InputFiles);

            // Add optimization flag if enabled
            if (project.OptimiseCode)
            {
                arguments.Add("-O");
            }

            // Add output file
            arguments.Add("-o");
            arguments.Add(project.OutputFile);

            return arguments;
        }
    }
}
