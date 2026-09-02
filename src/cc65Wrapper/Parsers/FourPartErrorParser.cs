namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Parses error lines with 4 parts (filename:line:type:error)
    /// </summary>
    public class FourPartErrorParser : IErrorLineParser
    {
        /// <inheritdoc/>
        public int Priority => 100;

        /// <summary>
        /// Determines whether the provided <paramref name="errorLine"/> matches the four-part
        /// cc65 error format: <c>filename:line:type:error</c>.
        /// </summary>
        /// <param name="errorLine">The raw error line to inspect. May be null or whitespace.</param>
        /// <returns>
        /// <c>true</c> when the input splits into exactly four ':'-separated parts and the second part
        /// can be parsed as an integer (line number); otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The method trims parts when validating. It performs a quick structural check only and
        /// does not validate the contents of the filename, type, or error message beyond trimming.
        /// </remarks>
        public bool CanParse(string errorLine)
        {
            if (string.IsNullOrWhiteSpace(errorLine))
                return false;

            var parts = errorLine.Split(':');
            return parts.Length == 4 && int.TryParse(parts[1].Trim(), out _);
        }

        /// <inheritdoc/>
        public Cc65Error Parse(string errorLine)
        {
            var parts = errorLine.Split(':');
            return new Cc65Error(
                Filename: parts[0].Trim(),
                LineNumber: int.Parse(parts[1].Trim()),
                Type: parts[2].Trim(),
                Error: parts[3].Trim()
            );
        }
    }
}
