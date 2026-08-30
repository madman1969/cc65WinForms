using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Abstraction for executing external commands
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes a command asynchronously
        /// </summary>
        /// <param name="executable">The executable to run</param>
        /// <param name="arguments">Command line arguments</param>
        /// <param name="environmentVariables">Optional environment variables</param>
        /// <param name="workingDirectory">Optional working directory</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The command result</returns>
        Task<Models.CommandResult> ExecuteAsync(
            string executable,
            IEnumerable<string> arguments,
            IDictionary<string, string> environmentVariables = null,
            string workingDirectory = null,
            CancellationToken cancellationToken = default);
    }
}
