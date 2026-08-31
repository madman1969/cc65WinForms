using System.Collections.Generic;

namespace cc65Wrapper.Models
{
    /// <summary>
    /// Result returned by the wrapper after attempting to launch an emulator process.
    /// </summary>
    /// <remarks>
    /// This record is immutable once constructed (uses C# init-only properties with <c>required</c>).
    /// - <see cref="Success"/> indicates whether the overall launch and run completed without detected failure.
    /// - <see cref="ExitCode"/> is the numeric exit code produced by the emulator process (conventionally 0 for success).
    /// - <see cref="Errors"/> contains any diagnostic or wrapper-level error messages captured while preparing or launching the emulator.
    /// - <see cref="StandardOutput"/> and <see cref="StandardError"/> contain the complete captured stdout/stderr streams from the emulator process.
    ///
    /// Callers should inspect <see cref="Success"/> first; if false, examine <see cref="Errors"/>, <see cref="StandardError"/>, and <see cref="ExitCode"/>
    /// to determine the cause and appropriate remediation.
    /// </remarks>
    /// <example>
    /// var result = new EmulatorLaunchResult
    /// {
    ///     Success = true,
    ///     ExitCode = 0,
    ///     Errors = Array.Empty&lt;string&gt;(),
    ///     StandardOutput = "Emulator started...",
    ///     StandardError = string.Empty
    /// };
    /// </example>
    public record EmulatorLaunchResult
    {
        /// <summary>
        /// Gets whether the launch and run completed successfully from the wrapper's perspective.
        /// </summary>
        /// <remarks>
        /// A value of <c>true</c> generally indicates the emulator process started and exited without wrapper-detected errors.
        /// Even when <c>true</c>, callers may still want to inspect <see cref="ExitCode"/> and output streams for domain-specific validation.
        /// </remarks>
        public required bool Success { get; init; }

        /// <summary>
        /// Gets the process exit code produced by the emulator.
        /// </summary>
        /// <remarks>
        /// The meaning of specific exit codes is defined by the emulator; 0 commonly means success.
        /// When <see cref="Success"/> is <c>false</c>, this value may aid in diagnosing failures.
        /// </remarks>
        public required int ExitCode { get; init; }

        /// <summary>
        /// Gets any wrapper-level or diagnostic error messages captured during preparation or launch.
        /// </summary>
        /// <remarks>
        /// This list may include validation errors, exceptions thrown while starting the process, or other non-emulator diagnostics.
        /// Use <see cref="Errors"/> before relying solely on process exit code when diagnosing failures.
        /// </remarks>
        public required IReadOnlyList<string> Errors { get; init; }

        /// <summary>
        /// Gets the text captured from the emulator's standard output stream.
        /// </summary>
        /// <remarks>
        /// May be an empty string if the emulator produced no output or if capturing was disabled.
        /// Prefer parsing or searching this text only after confirming it is populated to avoid unnecessary work.
        /// </remarks>
        public required string StandardOutput { get; init; }

        /// <summary>
        /// Gets the text captured from the emulator's standard error stream.
        /// </summary>
        /// <remarks>
        /// Typically contains error or diagnostic messages emitted by the emulator. May be empty.
        /// Combine inspection of this property with <see cref="Errors"/> and <see cref="ExitCode"/> for full diagnostics.
        /// </remarks>
        public required string StandardError { get; init; }
    }
}
