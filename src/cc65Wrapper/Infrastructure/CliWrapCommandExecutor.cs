using CliWrap;
using CliWrap.Buffered;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Models;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Logging;
using System;

namespace cc65Wrapper.Infrastructure
{
    /// <summary>
    /// Command executor implementation that runs external processes using <c>CliWrap</c>.
    /// </summary>
    /// <remarks>
    /// This class is a thin wrapper around <c>CliWrap</c> that:
    /// - Constructs and configures a <c>Cli</c> command,
    /// - Optionally applies working directory and environment variables,
    /// - Executes the command in a buffered fashion (capturing stdout/stderr),
    /// - Logs lifecycle events via the provided <see cref="ILogger{TCategoryName}"/>.
    ///
    /// Non-zero exit codes are not thrown as exceptions; instead the exit code and
    /// captured output are returned in a <see cref="Models.CommandResult"/>.
    /// </remarks>
    public class CliWrapCommandExecutor : ICommandExecutor
    {
        /// <summary>
        /// Logger used for command lifecycle and diagnostic messages.
        /// </summary>
        private readonly ILogger<CliWrapCommandExecutor> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CliWrapCommandExecutor"/> class.
        /// </summary>
        /// <param name="logger">
        /// Optional logger. If <c>null</c>, a default logger is created using
        /// <see cref="Cc65LoggerFactory.CreateLogger{TCategoryName}"/>
        /// </param>
        public CliWrapCommandExecutor(ILogger<CliWrapCommandExecutor> logger = null)
        {
            _logger = logger ?? Cc65LoggerFactory.CreateLogger<CliWrapCommandExecutor>();
        }

        /// <summary>
        /// Executes a command asynchronously and returns the captured exit code, standard output and standard error.
        /// </summary>
        /// <param name="executable">
        /// The executable name or full path to run (for example, <c>"cc65"</c>).
        /// </param>
        /// <param name="arguments">
        /// Sequence of arguments to pass to the executable. The sequence will be joined for logging, but passed to CliWrap as an enumerable.
        /// </param>
        /// <param name="environmentVariables">
        /// Optional environment variables to set for the process. If provided, the dictionary is copied to a read-only mapping
        /// and passed to the process environment.
        /// </param>
        /// <param name="workingDirectory">
        /// Optional working directory for the process. If <c>null</c>, the current process working directory is used.
        /// </param>
        /// <param name="cancellationToken">
        /// Token used to cancel the execution. If cancellation is requested, the underlying <c>CliWrap</c> execution will be cancelled
        /// and an <see cref="OperationCanceledException"/> may be thrown by the task.
        /// </param>
        /// <returns>
        /// A <see cref="Models.CommandResult"/> containing the process <see cref="Models.CommandResult.ExitCode"/>,
        /// captured <see cref="Models.CommandResult.StandardOutput"/>, and <see cref="Models.CommandResult.StandardError"/>.
        /// </returns>
        /// <remarks>
        /// - The method configures <c>CliWrap</c> with <c>CommandResultValidation.None</c> so non-zero exit codes are returned rather than thrown.
        /// - Standard output and error are captured via <c>ExecuteBufferedAsync</c>.
        /// - Lifecycle events (executing, completed, failed) are logged through the injected logger.
        /// </remarks>
        public async Task<Models.CommandResult> ExecuteAsync(
            string executable,
            IEnumerable<string> arguments,
            IDictionary<string, string> environmentVariables = null,
            string workingDirectory = null,
            CancellationToken cancellationToken = default)
        {
            var argumentString = string.Join(" ", arguments);
            _logger.LogCommandExecuting(executable, argumentString);

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

            if (result.ExitCode != 0)
            {
                _logger.LogCommandFailed(result.ExitCode, result.StandardError);
            }
            else
            {
                _logger.LogCommandCompleted(result.ExitCode);
            }

            return new Models.CommandResult(
                result.ExitCode,
                result.StandardOutput,
                result.StandardError);
        }
    }
}
