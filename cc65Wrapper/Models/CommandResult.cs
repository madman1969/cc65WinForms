namespace cc65Wrapper.Models
{
    /// <summary>
    /// Represents the result of executing a command or external process.
    /// </summary>
    /// <param name="ExitCode">The process exit code (conventionally 0 indicates success).</param>
    /// <param name="StandardOutput">The text captured from the process's standard output stream.</param>
    /// <param name="StandardError">The text captured from the process's standard error stream.</param>
    /// <remarks>
    /// Immutable record used to return execution results from implementations of
    /// <see cref="cc65Wrapper.Abstractions.ICommandExecutor"/>.
    /// </remarks>
    public record CommandResult(
        int ExitCode,
        string StandardOutput,
        string StandardError
    );
}
