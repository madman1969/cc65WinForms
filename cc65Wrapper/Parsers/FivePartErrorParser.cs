namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Parses error lines with 5 parts (filename:line:type:error:extra)
    /// </summary>
    public class FivePartErrorParser : IErrorLineParser
    {
        public int Priority => 75;

        /// <summary>
        /// Determines whether the provided <paramref name="errorLine"/> matches the
        /// expected five-part format produced by cc65: <c>filename:line:type:error:extra</c>.
        /// </summary>
        /// <param name="errorLine">The raw error line to test.</param>
        /// <returns>
        /// <c>true</c> when <paramref name="errorLine"/> is not null/whitespace, splits into
        /// exactly five colon-separated parts, and the second part can be parsed as an integer
        /// (the line number). Otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method trims whitespace around the line-number portion before attempting to parse it.
        /// It performs a simple structural check only and does not validate the contents of other parts.
        /// </remarks>
        public bool CanParse(string errorLine)
        {
            if (string.IsNullOrWhiteSpace(errorLine))
                return false;

            var parts = errorLine.Split(':');
            return parts.Length == 5 && int.TryParse(parts[1].Trim(), out _);
        }

        public Cc65Error Parse(string errorLine)
        {
            var parts = errorLine.Split(':');
            return new Cc65Error(
                Filename: parts[0].Trim(),
                LineNumber: int.Parse(parts[1].Trim()),
                Type: parts[2].Trim(),
                Error: $"{parts[3].Trim()}:{parts[4].Trim()}"
            );
        }
    }
}
