namespace cc65Wrapper.Models
{
    /// <summary>
    /// Represents the result of executing a command
    /// </summary>
    public record CommandResult(
        int ExitCode,
        string StandardOutput,
        string StandardError
    );
}
