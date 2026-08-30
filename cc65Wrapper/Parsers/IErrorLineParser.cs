namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Interface for parsing individual error lines
    /// </summary>
    public interface IErrorLineParser
    {
        /// <summary>
        /// Determines if this parser can handle the error line
        /// </summary>
        bool CanParse(string errorLine);

        /// <summary>
        /// Parses an error line into a Cc65Error
        /// </summary>
        Cc65Error Parse(string errorLine);

        /// <summary>
        /// Gets the priority of this parser (higher = checked first)
        /// </summary>
        int Priority { get; }
    }
}
