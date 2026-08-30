using System;
using System.IO;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Infrastructure
{
    /// <summary>
    /// File system implementation wrapper
    /// </summary>
    public class FileSystemWrapper : IFileSystem
    {
        /// <summary>
        /// Checks if a directory exists
        /// </summary>
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Gets the current directory
        /// </summary>
        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Sets the current directory
        /// </summary>
        public void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }
    }
}
