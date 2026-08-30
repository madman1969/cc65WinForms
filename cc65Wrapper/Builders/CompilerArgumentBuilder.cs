using System.Collections.Generic;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Builders
{
    /// <summary>
    /// Builds command-line arguments for the CC65 compiler
    /// </summary>
    public class CompilerArgumentBuilder : IArgumentBuilder<CC65Project>
    {
        /// <summary>
        /// Builds compiler arguments from a project
        /// </summary>
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
