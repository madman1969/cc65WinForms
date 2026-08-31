namespace cc65Wrapper
{
    /// <summary>
    /// Represents a single compilation diagnostic produced by the CL65 driver from the cc65 toolchain.
    /// </summary>
    /// <remarks>
    /// This record models an individual error/warning produced while assembling or linking source files.
    /// Instances are immutable and intended to be a lightweight container for tooling and UI code that
    /// displays diagnostics to the user (for example in an Error List or a compiler output window).
    /// </remarks>
    /// <param name="Filename">The source filename where the diagnostic occurred. Typically a relative or absolute path.</param>
    /// <param name="LineNumber">The 1-based line number in <paramref name="Filename"/> where the issue was reported. Use 0 if not applicable.</param>
    /// <param name="Type">A short text describing the diagnostic type or severity (for example: "error", "warning", or a compiler-specific code).</param>
    /// <param name="Error">The human-readable diagnostic message produced by CL65.</param>
    /// <example>
    /// <code language="csharp">
    /// var diag = new Cc65Error("src\\main.asm", 42, "error", "Undefined symbol 'FOO'");
    /// Console.WriteLine($"{diag.Filename}({diag.LineNumber}): {diag.Type}: {diag.Error}");
    /// </code>
    /// </example>
    public record Cc65Error(
        string Filename,
        int LineNumber,
        string Type,
        string Error
    );
}
