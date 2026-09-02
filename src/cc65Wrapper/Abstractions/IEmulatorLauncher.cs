using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Models;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Defines an abstraction for launching emulators for a CC65 project.
    /// Implementations are responsible for starting the configured emulator process,
    /// wiring up any required arguments or working directories from the provided
    /// <see cref="CC65Project"/> and <see cref="Cc65Emulators"/> configuration,
    /// and returning an <see cref="EmulatorLaunchResult"/> describing the outcome.
    /// </summary>
    public interface IEmulatorLauncher
    {
        /// <summary>
        /// Launches an emulator for the specified project asynchronously.
        /// </summary>
        /// <param name="project">
        /// The <see cref="CC65Project"/> to run in the emulator. Must contain any
        /// build outputs and runtime settings required by the emulator.
        /// </param>
        /// <param name="emulators">
        /// The <see cref="Cc65Emulators"/> configuration describing which emulator
        /// to use and its command-line options, paths, or mappings.
        /// </param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> to observe while waiting for the
        /// launch operation to complete. If cancellation is requested the operation
        /// should cease any work and attempt to return promptly.
        /// </param>
        /// <returns>
        /// A <see cref="Task{EmulatorLaunchResult}"/> that completes with an
        /// <see cref="EmulatorLaunchResult"/> describing whether the emulator was
        /// started successfully and any associated process or error information.
        /// </returns>
        /// <remarks>
        /// Implementations should:
        /// - Validate input arguments and throw <see cref="System.ArgumentNullException"/>
        ///   for null <paramref name="project"/> or <paramref name="emulators"/>.
        /// - Respect <paramref name="cancellationToken"/> and throw
        ///   <see cref="System.OperationCanceledException"/> if cancellation is observed
        ///   before the operation completes.
        /// - Not block the calling thread; long-running work should be performed
        ///   asynchronously.
        /// - Populate the returned <see cref="EmulatorLaunchResult"/> to convey
        ///   success/failure and any diagnostics rather than relying solely on exceptions.
        /// </remarks>
        Task<EmulatorLaunchResult> LaunchAsync(
            CC65Project project,
            Cc65Emulators emulators,
            CancellationToken cancellationToken = default);
    }
}
