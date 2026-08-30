using System.Collections.Generic;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Abstraction for building command-line arguments
    /// </summary>
    /// <typeparam name="TOptions">The type of options to build arguments from</typeparam>
    public interface IArgumentBuilder<in TOptions>
    {
        /// <summary>
        /// Builds command-line arguments from the specified options
        /// </summary>
        /// <param name="options">The options to build from</param>
        /// <returns>The command-line arguments</returns>
        IEnumerable<string> Build(TOptions options);
    }
}
