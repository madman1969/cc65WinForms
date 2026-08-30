namespace cc65Wrapper
{
    /// <summary>
    /// Represents an individual compilation error raised by CL65
    /// </summary>
    public record Cc65Error(
        string Filename,
        int LineNumber,
        string Type,
        string Error
    );
}
