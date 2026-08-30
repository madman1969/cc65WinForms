namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Parses error lines with 5 parts (filename:line:type:error:extra)
    /// </summary>
    public class FivePartErrorParser : IErrorLineParser
    {
        public int Priority => 75;

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
