using System;

namespace cc65Wrapper.Models
{
    /// <summary>
    /// Event arguments for build progress updates
    /// </summary>
    public class BuildProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the progress message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the percentage complete (0-100)
        /// </summary>
        public double PercentComplete { get; }

        /// <summary>
        /// Gets the current build phase
        /// </summary>
        public BuildPhase Phase { get; }

        /// <summary>
        /// Initializes a new instance of the BuildProgressEventArgs class
        /// </summary>
        /// <param name="message">A human-readable message describing the current state.</param>
        /// <param name="percentComplete">Progress percentage (0-100).</param>
        /// <param name="phase">The logical build phase this update relates to.</param>
        public BuildProgressEventArgs(string message, double percentComplete, BuildPhase phase)
        {
            Message = message;
            PercentComplete = percentComplete;
            Phase = phase;
        }
    }

    /// <summary>
    /// Represents the logical phases of a build operation reported via <see cref="BuildProgressEventArgs"/>.
    /// Handlers receiving progress updates can use these values to determine which part of the pipeline
    /// the message relates to (initialization, validation, argument construction, compilation, parsing, or completion).
    /// </summary>
    /// <remarks>
    /// The phases describe a typical build pipeline but do not strictly enforce ordering — implementations
    /// may report phases in a different sequence or repeat phases as needed. Do not assume a phase implies
    /// success; check other status indicators or messages for build result details.
    /// </remarks>
    public enum BuildPhase
    {
        /// <summary>
        /// Preparing the build environment: loading configuration, allocating resources and performing initial setup.
        /// </summary>
        Initializing,

        /// <summary>
        /// Validating project inputs, paths and dependencies. Validation errors or missing files are typically reported here.
        /// </summary>
        ValidatingProject,

        /// <summary>
        /// Assembling command-line arguments and invocation parameters for the underlying tools (compiler/assembler/linker).
        /// </summary>
        BuildingArguments,

        /// <summary>
        /// Executing the compiler/assembler. Multiple progress updates can be reported while compiling.
        /// </summary>
        Compiling,

        /// <summary>
        /// Parsing tool output for errors, warnings and diagnostics; used to surface parsed issues to the UI or logs.
        /// </summary>
        ParsingErrors,

        /// <summary>
        /// Build operation has completed. Typically paired with <see cref="BuildProgressEventArgs.PercentComplete"/> == 100,
        /// and the <see cref="BuildProgressEventArgs.Message"/> may indicate success or failure.
        /// </summary>
        Completed
    }
}
