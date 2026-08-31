namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Parses error lines with 3 parts (filename:type:error)
    /// </summary>
    public class ThreePartErrorParser : IErrorLineParser
    {
        public int Priority => 50;

        /// <summary>
        /// Determines whether the given <paramref name="errorLine"/> can be parsed by this parser.
        /// </summary>
        /// <param name="errorLine">An error line expected in the format <c>filename:type:error</c>.</param>
        /// <returns>
        /// <c>true</c> when <paramref name="errorLine"/> is not null/whitespace and splits into exactly three
        /// colon-separated parts; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This performs a lightweight check only: it verifies the presence of exactly three parts after
        /// splitting on ':' and does not validate the semantic contents of each part (for example, whether
        /// the filename is a valid path or whether the type is a known category).
        /// </remarks>
        public bool CanParse(string errorLine)
        {
            if (string.IsNullOrWhiteSpace(errorLine))
                return false;

            var parts = errorLine.Split(':');
            return parts.Length == 3;
        }

        public Cc65Error Parse(string errorLine)
        {
            var parts = errorLine.Split(':');
            return new Cc65Error(
                Filename: parts[0].Trim(),
                LineNumber: 0,
                Type: parts[1].Trim(),
                Error: parts[2].Trim()
            );
        }
    }
}
