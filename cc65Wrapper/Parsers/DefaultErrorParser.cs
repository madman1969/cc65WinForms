using System.Linq;

namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Default fallback parser for error lines that don't match other patterns
    /// </summary>
    public class DefaultErrorParser : IErrorLineParser
    {
        public int Priority => 0;

        public bool CanParse(string errorLine)
        {
            // Always returns true as fallback
            return !string.IsNullOrWhiteSpace(errorLine);
        }

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
