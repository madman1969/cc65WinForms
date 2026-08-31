using System.Linq;

namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Default fallback parser for error lines that don't match other patterns.
    /// This parser is used when no higher-priority parser recognizes the input.
    /// It attempts a best-effort parse using colon-separated segments:
    /// <c>filename:type:message</c>. If the line does not contain colons it will
    /// treat the whole line as an error message.
    /// </summary>
    public class DefaultErrorParser : IErrorLineParser
    {
        /// <summary>
        /// Gets the parser priority. Lower values indicate lower priority.
        /// This parser returns <c>0</c> so it acts as the final fallback.
        /// </summary>
        public int Priority => 0;

        /// <summary>
        /// Determines whether this parser can parse the provided error line.
        /// </summary>
        /// <param name="errorLine">The raw error line produced by the tool.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="errorLine"/> is not null, empty, or whitespace;
        /// otherwise <c>false</c>. This parser returns <c>true</c> for any non-empty line
        /// so it can serve as the fallback parser.
        /// </returns>
        public bool CanParse(string errorLine)
        {
            // Always returns true as fallback
            return !string.IsNullOrWhiteSpace(errorLine);
        }

        /// <summary>
        /// Parses a single error line into a <see cref="Cc65Error"/>
        /// </summary>
        /// <param name="errorLine">The raw error line to parse.</param>
        /// <returns>
        /// A <see cref="Cc65Error"/> instance populated from the parsed components.
        /// If the line contains at least one colon the method interprets the first segment
        /// as the filename, the second as the type, and the remainder (joined by ':') as the message.
        /// If no colon is present the entire line is returned as the error message with an empty filename
        /// and <c>Type</c> set to "Error". The returned <c>LineNumber</c> is always <c>0</c> since
        /// this parser does not extract line numbers.
        /// </returns>
        public Cc65Error Parse(string errorLine)
        {
            var parts = errorLine.Split(':');

            if (parts.Length >= 2)
            {
                var errorText = string.Join(":", parts.Skip(2));
                return new Cc65Error(
                    Filename: parts[0].Trim(),
                    LineNumber: 0,
                    Type: parts[1].Trim(),
                    Error: errorText
                );
            }

            // If it doesn't even have colons, treat entire line as error
            return new Cc65Error(
                Filename: string.Empty,
                LineNumber: 0,
                Type: "Error",
                Error: errorLine.Trim()
            );
        }
    }
}
