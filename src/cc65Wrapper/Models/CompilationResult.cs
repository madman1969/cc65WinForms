using System;
using System.Collections.Generic;

namespace cc65Wrapper.Models
{
    /// <summary>
    /// Represents the result of a single compiler invocation.
    /// </summary>
    /// <remarks>
    /// This record contains both machine-readable information (such as the
    /// <see cref="ExitCode"/> and parsed <see cref="Errors"/>) and raw textual
    /// captures of the compiler's output streams (<see cref="StandardOutput"/>
    /// and <see cref="StandardError"/>). Use <see cref="Success"/> to quickly
    /// determine whether the compilation completed successfully.
    /// </remarks>
    public record CompilationResult
    {
        /// <summary>
        /// True when the compiler reported success (no fatal errors).
        /// </summary>
        /// <remarks>
        /// This flag is intended to represent the overall success state of the
        /// compilation operation and is typically derived from the compiler's
        /// exit code and/or the presence of errors in <see cref="Errors"/>.
        /// </remarks>
        public required bool Success { get; init; }

        /// <summary>
        /// The numeric exit code returned by the compiler process.
        /// </summary>
        /// <remarks>
        /// A value of zero commonly indicates success; non-zero values indicate
        /// different classes of failure as defined by the compiler. The
        /// <see cref="Failed(string)"/> factory sets <c>-1</c> when an internal
        /// failure prevents invoking the compiler.
        /// </remarks>
        public required int ExitCode { get; init; }

        /// <summary>
        /// A read-only list of parsed compiler diagnostics.
        /// </summary>
        /// <remarks>
        /// Each item is a <see cref="Cc65Error"/> representing a single error,
        /// warning, or informational message emitted by the compiler. This list
        /// may be empty when no diagnostics were produced.
        /// </remarks>
        public required IReadOnlyList<Cc65Error> Errors { get; init; }

        /// <summary>
        /// The full contents captured from the compiler's standard output stream.
        /// </summary>
        /// <remarks>
        /// This text is provided primarily for debugging, logging, or displaying
        /// to the user when the parsed <see cref="Errors"/> do not contain
        /// enough detail.
        /// </remarks>
        public required string StandardOutput { get; init; }

        /// <summary>
        /// The full contents captured from the compiler's standard error stream.
        /// </summary>
        /// <remarks>
        /// The standard error often contains additional diagnostic information
        /// and raw error messages that may not have been parsed into
        /// <see cref="Errors"/>.
        /// </remarks>
        public required string StandardError { get; init; }

        /// <summary>
        /// The elapsed time taken to perform the compilation operation.
        /// </summary>
        /// <remarks>
        /// Useful for telemetry and performance measurements. When the compiler
        /// was not invoked due to a precondition failure, this may be
        /// <see cref="TimeSpan.Zero"/>.
        /// </remarks>
        public required TimeSpan Duration { get; init; }

        /// <summary>
        /// Creates a <see cref="CompilationResult"/> representing a failure that
        /// occurred before or outside of a normal compiler process run.
        /// </summary>
        /// <param name="error">
        /// A human-readable error message describing the failure (for example,
        /// an exception message or an internal validation error).
        /// </param>
        /// <returns>
        /// A <see cref="CompilationResult"/> populated with a single fatal
        /// <see cref="Cc65Error"/>, an exit code of -1, empty standard output,
        /// and the provided message in <see cref="StandardError"/>.
        /// </returns>
        /// <remarks>
        /// This helper is used when the compilation step could not be executed
        /// (for example, when the compiler executable could not be found or an
        /// unexpected exception occurred). Callers can rely on the returned
        /// object's <see cref="Success"/> being <c>false</c>.
        /// </remarks>
        public static CompilationResult Failed(string error)
        {
            return new CompilationResult
            {
                Success = false,
                ExitCode = -1,
                Errors = new[] { new Cc65Error("", 0, "Fatal", error) },
                StandardOutput = string.Empty,
                StandardError = error,
                Duration = TimeSpan.Zero
            };
        }
    }
}
