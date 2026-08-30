using System;
using System.IO;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Infrastructure
{
    /// <summary>
    /// Provides a disposable context for temporarily changing the working directory
    /// </summary>
    public sealed class WorkingDirectoryContext : IDisposable
    {
        private readonly string _originalDirectory;
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Initializes a new instance and changes to the specified working directory
        /// </summary>
        /// <param name="workingDirectory">The directory to change to</param>
        /// <param name="fileSystem">The file system implementation</param>
        /// <exception cref="ArgumentNullException">Thrown when fileSystem is null</exception>
        /// <exception cref="ArgumentException">Thrown when workingDirectory is empty</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when the directory doesn't exist</exception>
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
        /// Restores the original working directory
        /// </summary>
        public void Dispose()
        {
            _fileSystem.SetCurrentDirectory(_originalDirectory);
        }
    }
}
