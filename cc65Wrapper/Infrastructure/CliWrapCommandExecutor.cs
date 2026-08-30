using CliWrap;
using CliWrap.Buffered;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Models;

namespace cc65Wrapper.Infrastructure
{
    /// <summary>
    /// Command executor implementation using CliWrap
    /// </summary>
    public class CliWrapCommandExecutor : ICommandExecutor
    {
        /// <summary>
        /// Executes a command asynchronously
        /// </summary>
        public async Task<Models.CommandResult> ExecuteAsync(
            string executable,
            IEnumerable<string> arguments,
            IDictionary<string, string> environmentVariables = null,
            string workingDirectory = null,
            CancellationToken cancellationToken = default)
        {
            var command = Cli.Wrap(executable)
                .WithArguments(arguments)
                .WithValidation(CommandResultValidation.None);

            if (workingDirectory != null)
                command = command.WithWorkingDirectory(workingDirectory);

            if (environmentVariables != null && environmentVariables.Any())
            {
                var readOnlyEnvVars = environmentVariables.ToDictionary(k => k.Key, v => (string?)v.Value);
                command = command.WithEnvironmentVariables(readOnlyEnvVars);
            }

            var result = await command
                .ExecuteBufferedAsync(cancellationToken)
                .ConfigureAwait(false);

            return new Models.CommandResult(
                result.ExitCode,
                result.StandardOutput,
                result.StandardError);
        }
    }
}
