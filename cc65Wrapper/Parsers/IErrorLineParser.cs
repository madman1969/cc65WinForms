namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Parses a single line of cc65 tool output and converts it into a <see cref="Cc65Error"/>.
    /// </summary>
    /// <remarks>
    /// Implementations should recognize specific error/warning line formats produced by the cc65 toolchain.
    /// Typical usage is to call <see cref="CanParse(string)"/> to determine applicability before calling
    /// <see cref="Parse(string)"/>. Parsers are expected to be lightweight, stateless, and reentrant so they
    /// can be reused across files and threads.
    /// </remarks>
    /// <example>
    /// Example: choose the first parser (highest priority first) that can parse a given line.
    /// <code><![CDATA[
    /// // Parsers with higher Priority values are evaluated first.
    /// var parsers = new[] { parserA /* Priority=100 */, parserB /* Priority=50 */ };
    /// foreach (var line in lines)
    /// {
    ///     var parser = parsers.OrderByDescending(p => p.Priority).FirstOrDefault(p => p.CanParse(line));
    ///     if (parser != null)
    ///     {
    ///         var err = parser.Parse(line);
    ///         // handle err
    ///     }
    /// }
    /// ]]></code>
    /// </example>
    public interface IErrorLineParser
    {
        /// <summary>
        /// Determines whether this parser can handle the specified cc65 output line.
        /// </summary>
        /// <param name="errorLine">A single line of output from cc65. Implementations may assume non-null input.</param>
        /// <returns>
        /// <c>true</c> if this parser recognizes the format of <paramref name="errorLine"/>; otherwise <c>false</c>.
        /// </returns>
        bool CanParse(string errorLine);

        /// <summary>
        /// Parses the specified cc65 output line and returns a corresponding <see cref="Cc65Error"/>.
        /// </summary>
        /// <param name="errorLine">A single line of cc65 output that this parser has indicated it can parse.</param>
        /// <returns>A <see cref="Cc65Error"/> representing the parsed information (file, line, severity, message, etc.).</returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="errorLine"/> is not in an expected format. Call <see cref="CanParse(string)"/>
        /// before invoking this method to avoid this exception.
        /// </exception>
        Cc65Error Parse(string errorLine);

        /// <summary>
        /// Priority used when multiple parsers are available; higher values are checked before lower values.
        /// </summary>
        /// <remarks>
        /// Use this to ensure more specific or greedy parsers run prior to more general ones. The value is
        /// not constrained to a fixed range; zero can serve as a sensible default.
        /// </remarks>
        int Priority { get; }
    }
}
