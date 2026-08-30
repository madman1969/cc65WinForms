using System.Collections.Generic;

namespace cc65Wrapper.Models
{
    /// <summary>
    /// Represents the result of launching an emulator
    /// </summary>
    public record EmulatorLaunchResult
    {
        /// <summary>
        /// Gets whether the launch was successful
        /// </summary>
        public required bool Success { get; init; }

        /// <summary>
        /// Gets the exit code from the emulator
        /// </summary>
        public required int ExitCode { get; init; }

        /// <summary>
        /// Gets any error messages
        /// </summary>
        public required IReadOnlyList<string> Errors { get; init; }

        /// <summary>
        /// Gets the standard output
        /// </summary>
        public required string StandardOutput { get; init; }

        /// <summary>
        /// Gets the standard error
        /// </summary>
        public required string StandardError { get; init; }
    }
}
