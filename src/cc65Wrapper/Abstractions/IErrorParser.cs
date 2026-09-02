using System.Collections.Generic;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Parses raw compiler output from the cc65 toolchain and produces structured error objects.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface convert textual compiler messages (errors, warnings, notes)
    /// into <see cref="Cc65Error"/> instances so callers can inspect file paths, line numbers,
    /// severity and original message text in a machine-friendly way.
    ///
    /// Implementations should:
    /// - Be resilient to variations in cc65 output formatting.
    /// - Prefer line-by-line parsing and skip unrecognized lines rather than fail the entire parse.
    /// - Be safe for concurrent use when possible (document thread-safety per implementation).
    /// - Return an empty list if no parseable errors are found or if input is null/empty.
    /// </remarks>
    public interface IErrorParser
    {
        /// <summary>
        /// Parses compiler output into a list of <see cref="Cc65Error"/> objects.
        /// </summary>
        /// <param name="errorOutput">
        /// The raw compiler output to parse (for example, stderr or combined stdout/stderr).
        /// May be null or an empty string; implementations should handle this and return an empty list.
        /// </param>
        /// <returns>
        /// A list of parsed <see cref="Cc65Error"/> instances. The list will be empty when no parseable
        /// messages are found or when <paramref name="errorOutput"/> is null/empty.
        /// </returns>
        /// <remarks>
        /// Parsers should preserve the original message text in the resulting <see cref="Cc65Error"/>
        /// and populate location and severity fields when information is available in the input.
        /// </remarks>
        List<Cc65Error> Parse(string errorOutput);
    }
}
