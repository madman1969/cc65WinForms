using System.Collections.Generic;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Abstraction for parsing compiler error output
    /// </summary>
    public interface IErrorParser
    {
        /// <summary>
        /// Parses error output into structured error objects
        /// </summary>
        /// <param name="errorOutput">The error output to parse</param>
        /// <returns>A list of parsed errors</returns>
        List<Cc65Error> Parse(string errorOutput);
    }
}
