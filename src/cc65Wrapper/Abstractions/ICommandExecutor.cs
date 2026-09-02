using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Provides an abstraction for running external processes/commands.
    /// Implementations are responsible for starting the process, capturing
    /// output, and producing a <see cref="Models.CommandResult"/> describing
    /// the outcome.
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes an external command asynchronously.
        /// </summary>
        /// <param name="executable">
        /// The path or name of the executable to run. This should not include
        /// command-line arguments.
        /// </param>
        /// <param name="arguments">
        /// A sequence of command-line arguments to pass to the executable. Each
        /// entry represents a single argument; implementations should handle any
        /// necessary quoting/escaping when constructing the process start info.
        /// </param>
        /// <param name="environmentVariables">
        /// Optional collection of environment variables to set for the process.
        /// If <c>null</c>, the process should inherit the parent process's
        /// environment. Keys are variable names and values are variable values.
        /// </param>
        /// <param name="workingDirectory">
        /// Optional working directory for the launched process. If <c>null</c>,
        /// the current working directory of the host process should be used.
        /// </param>
        /// <param name="cancellationToken">
        /// Token to observe for cancellation. Implementations should respect this
        /// token and attempt to terminate the launched process promptly if
        /// cancellation is requested. Cancellation may result in an
        /// <see cref="System.OperationCanceledException"/> being thrown or a
        /// canceled task result, depending on the implementation.
        /// </param>
        /// <returns>
        /// A task that completes with a <see cref="Models.CommandResult"/> containing
        /// the process exit code, captured standard output and standard error,
        /// and any additional execution metadata. See <see cref="Models.CommandResult"/>
        /// for details.
        /// </returns>
        Task<Models.CommandResult> ExecuteAsync(
            string executable,
            IEnumerable<string> arguments,
            IDictionary<string, string> environmentVariables = null,
            string workingDirectory = null,
            CancellationToken cancellationToken = default);
    }
}
