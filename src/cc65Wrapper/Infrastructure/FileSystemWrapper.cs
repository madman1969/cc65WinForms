using System;
using System.IO;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Infrastructure
{
    /// <summary>
    /// A small wrapper around <see cref="System.IO.Directory"/> to provide an
    /// <see cref="IFileSystem"/> implementation for production and testable code.
    /// </summary>
    /// <remarks>
    /// This class delegates directly to the static <see cref="Directory"/> APIs.
    /// It exists to allow injecting file-system behavior in unit tests and to
    /// centralize any future platform-specific handling or instrumentation.
    /// </remarks>
    public class FileSystemWrapper : IFileSystem
    {
        /// <summary>
        /// Determines whether the given directory exists.
        /// </summary>
        /// <param name="path">The directory path to check. May be <c>null</c> or empty.</param>
        /// <returns>
        /// <c>true</c> if the directory exists and is accessible; otherwise <c>false</c>.
        /// Note: this method delegates to <see cref="Directory.Exists(string)"/>, which
        /// returns <c>false</c> for <c>null</c>, empty, or invalid paths rather than throwing.
        /// </returns>
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Gets the full path of the current working directory.
        /// </summary>
        /// <returns>
        /// The absolute path of the current working directory for the process.
        /// </returns>
        /// <remarks>
        /// This method delegates to <see cref="Directory.GetCurrentDirectory"/>.
        /// Callers should be aware that the current directory is a process-wide setting
        /// and may change if another part of the process calls <see cref="SetCurrentDirectory"/>.
        /// </remarks>
        /// <exception cref="IOException">The current directory cannot be retrieved.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have required permissions.</exception>
        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Sets the current working directory for the process.
        /// </summary>
        /// <param name="path">The directory path to set as the current working directory.</param>
        /// <remarks>
        /// This method delegates to <see cref="Directory.SetCurrentDirectory(string)"/>.
        /// Changing the current directory affects the entire process and may impact other
        /// components that rely on the current working directory.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <c>null</c>.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path does not exist.</exception>
        /// <exception cref="IOException">An I/O error occurred while setting the directory.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have required permissions.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have required permissions.</exception>
        public void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }
    }
}
