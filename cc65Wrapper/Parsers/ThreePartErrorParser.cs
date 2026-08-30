namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Parses error lines with 3 parts (filename:type:error)
    /// </summary>
    public class ThreePartErrorParser : IErrorLineParser
    {
        public int Priority => 50;

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
