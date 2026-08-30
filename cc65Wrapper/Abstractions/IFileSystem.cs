namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Abstraction for file system operations
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Checks if a directory exists
        /// </summary>
        bool DirectoryExists(string path);

        /// <summary>
        /// Gets the current directory
        /// </summary>
        string GetCurrentDirectory();

        /// <summary>
        /// Sets the current directory
        /// </summary>
        void SetCurrentDirectory(string path);
    }
}
