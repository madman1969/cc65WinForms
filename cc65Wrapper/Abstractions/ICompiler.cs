using System;
using System.Threading;
using System.Threading.Tasks;
using cc65Wrapper.Models;

namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Abstraction for CC65 compiler operations
    /// </summary>
    public interface ICompiler
    {
        /// <summary>
        /// Raised when compilation progress changes
        /// </summary>
        event EventHandler<BuildProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Compiles a CC65 project
        /// </summary>
        /// <param name="project">The project to compile</param>
        /// <param name="progress">Optional progress reporter</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The compilation result</returns>
        Task<CompilationResult> CompileAsync(
            CC65Project project,
            IProgress<BuildProgressEventArgs> progress = null,
            CancellationToken cancellationToken = default);
    }
}
