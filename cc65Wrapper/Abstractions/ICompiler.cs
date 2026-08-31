using System;
using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Models;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Defines an abstraction for compiling CC65 projects and reporting build progress.
    /// Implementations perform the actual invocation of the CC65 toolchain and produce
    /// a <see cref="CompilationResult"/> describing success, diagnostics and outputs.
    /// </summary>
    public interface ICompiler
    {
        /// <summary>
        /// Raised when compilation progress changes.
        /// Subscribers receive <see cref="BuildProgressEventArgs"/> updates describing
        /// the current step, percentage complete and any textual status.
        /// Implementations may raise this event from a background thread; consumers
        /// receiving UI updates should marshal to the UI thread if required.
        /// </summary>
        event EventHandler<BuildProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Asynchronously compiles the specified <see cref="CC65Project"/>.
        /// </summary>
        /// <param name="project">The project to compile. This parameter must not be <c>null</c>.</param>
        /// <param name="progress">
        /// Optional <see cref="IProgress{T}"/> reporter that receives <see cref="BuildProgressEventArgs"/>
        /// as the build proceeds. If <c>null</c>, progress will still be published via the
        /// <see cref="ProgressChanged"/> event when supported by the implementation.
        /// </param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> used to cancel the compilation operation.
        /// Implementations should observe this token and attempt to stop work promptly
        /// when cancellation is requested.
        /// </param>
        /// <returns>
        /// A task that completes with a <see cref="CompilationResult"/> describing the outcome
        /// (success, errors, warnings, and produced artifacts). The task completes successfully
        /// when compilation finishes (even if there are build errors); it completes as
        /// <see cref="OperationCanceledException"/> if the operation is canceled.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="project"/> is <c>null</c>.</exception>
        /// <exception cref="OperationCanceledException">Thrown if the operation was canceled via <paramref name="cancellationToken"/>.</exception>
        /// <remarks>
        /// Implementations should:
        /// - Validate the provided project and throw <see cref="ArgumentNullException"/> for a null reference.
        /// - Honor <paramref name="cancellationToken"/> and cease work when cancellation is requested.
        /// - Report incremental progress via both the optional <paramref name="progress"/> reporter
        ///   and the <see cref="ProgressChanged"/> event (if supported).
        /// - Not block the calling thread; all long-running work must be performed asynchronously.
        /// </remarks>
        Task<CompilationResult> CompileAsync(
            CC65Project project,
            IProgress<BuildProgressEventArgs> progress = null,
            CancellationToken cancellationToken = default);
    }
}
