using System;
using System.Collections.Generic;

namespace cc65Wrapper.Models
{
    /// <summary>
    /// Represents the result of a compilation operation
    /// </summary>
    public record CompilationResult
    {
        /// <summary>
        /// Gets whether the compilation was successful
        /// </summary>
        public required bool Success { get; init; }

        /// <summary>
        /// Gets the exit code from the compiler
        /// </summary>
        public required int ExitCode { get; init; }

        /// <summary>
        /// Gets the list of compilation errors
        /// </summary>
        public required IReadOnlyList<Cc65Error> Errors { get; init; }

        /// <summary>
        /// Gets the standard output from the compiler
        /// </summary>
        public required string StandardOutput { get; init; }

        /// <summary>
        /// Gets the standard error from the compiler
        /// </summary>
        public required string StandardError { get; init; }

        /// <summary>
        /// Gets the duration of the compilation
        /// </summary>
        public required TimeSpan Duration { get; init; }

        /// <summary>
        /// Creates a failed compilation result
        /// </summary>
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
