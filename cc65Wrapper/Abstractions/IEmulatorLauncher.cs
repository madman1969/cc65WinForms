using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Models;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Abstraction for launching emulators
    /// </summary>
    public interface IEmulatorLauncher
    {
        /// <summary>
        /// Launches an emulator for the specified project
        /// </summary>
        /// <param name="project">The project to launch</param>
        /// <param name="emulators">Emulator configuration</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The launch result</returns>
        Task<EmulatorLaunchResult> LaunchAsync(
            CC65Project project,
            Cc65Emulators emulators,
            CancellationToken cancellationToken = default);
    }
}
