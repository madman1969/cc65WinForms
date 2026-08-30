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
        public BuildProgressEventArgs(string message, double percentComplete, BuildPhase phase)
        {
            Message = message;
            PercentComplete = percentComplete;
            Phase = phase;
        }
    }

    /// <summary>
    /// Represents the phases of a build operation
    /// </summary>
    public enum BuildPhase
    {
        /// <summary>
        /// Initializing the build
        /// </summary>
        Initializing,

        /// <summary>
        /// Validating the project
        /// </summary>
        ValidatingProject,

        /// <summary>
        /// Building command-line arguments
        /// </summary>
        BuildingArguments,

        /// <summary>
        /// Compiling the project
        /// </summary>
        Compiling,

        /// <summary>
        /// Parsing errors
        /// </summary>
        ParsingErrors,

        /// <summary>
        /// Build completed
        /// </summary>
        Completed
    }
}
