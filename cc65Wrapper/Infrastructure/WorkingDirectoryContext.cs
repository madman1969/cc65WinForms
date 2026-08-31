using System;
using System.IO;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Infrastructure
{

#pragma warning disable CS1734 // XML comment has a paramref tag, but there is no parameter by that name
    /// <summary>
    /// Provides a disposable context for temporarily changing the process working directory.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Creating an instance of <see cref="WorkingDirectoryContext"/> stores the current working
    /// directory (via the provided <see cref="IFileSystem"/>) and then changes the working
    /// directory to the supplied <paramref name="workingDirectory"/>. Calling <see cref="Dispose"/>
    /// restores the original working directory.
    /// </para>
    /// <para>
    /// This type is intended to be used with a <c>using</c> statement to guarantee restoration:
    /// <code language="csharp">
    /// using (new WorkingDirectoryContext(path, fileSystem))
    /// {
    ///     // operations that require the working directory to be 'path'
    /// }
    /// // original working directory is restored here
    /// </code>
    /// </para>
    /// <para>
    /// The class relies on the <see cref="IFileSystem"/> abstraction to allow easier testing
    /// and to avoid direct dependencies on <see cref="System.IO.Directory"/> operations.
    /// </para>
    /// </remarks>
    /// <threadsafety>
    /// Not thread-safe. Do not share a single instance across threads while it is active.
    /// </threadsafety>
    public sealed class WorkingDirectoryContext : IDisposable
#pragma warning restore CS1734 // XML comment has a paramref tag, but there is no parameter by that name
    {
        private readonly string _originalDirectory;
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Initializes a new <see cref="WorkingDirectoryContext"/> and changes the current working directory.
        /// </summary>
        /// <param name="workingDirectory">The target directory to switch to. Must be non-empty and exist according to <paramref name="fileSystem"/>.</param>
        /// <param name="fileSystem">An implementation of <see cref="IFileSystem"/> used to query and set the current directory.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="fileSystem"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="workingDirectory"/> is <c>null</c>, empty, or consists only of white-space characters.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when <paramref name="workingDirectory"/> does not exist according to <paramref name="fileSystem"/>.</exception>
        public WorkingDirectoryContext(string workingDirectory, IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

            if (string.IsNullOrWhiteSpace(workingDirectory))
                throw new ArgumentException("Working directory cannot be empty", nameof(workingDirectory));

            if (!_fileSystem.DirectoryExists(workingDirectory))
                throw new DirectoryNotFoundException($"Working directory not found: {workingDirectory}");

            _originalDirectory = _fileSystem.GetCurrentDirectory();
            _fileSystem.SetCurrentDirectory(workingDirectory);
        }

        /// <summary>
        /// Restores the original working directory that was active when this instance was created.
        /// </summary>
        /// <remarks>
        /// Calling <see cref="Dispose"/> will set the working directory back to the value captured
        /// at construction time. If the underlying <see cref="IFileSystem"/> implementation throws
        /// during <see cref="IFileSystem.SetCurrentDirectory"/>, that exception will propagate to the caller.
        /// </remarks>
        public void Dispose()
        {
            _fileSystem.SetCurrentDirectory(_originalDirectory);
        }
    }
}
