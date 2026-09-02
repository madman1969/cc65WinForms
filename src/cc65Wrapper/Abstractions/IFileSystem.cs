namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Provides an abstraction over basic file system operations used by the application.
    /// </summary>
    /// <remarks>
    /// Abstracting these operations makes code easier to unit test and allows swapping
    /// implementations (for example, a mock file system or a platform-specific implementation).
    /// Implementations should aim to mirror the behavior of the corresponding members in
    /// <c>System.IO</c> where applicable.
    /// </remarks>
    public interface IFileSystem
    {
        /// <summary>
        /// Determines whether the specified directory exists.
        /// </summary>
        /// <param name="path">The path to the directory to check. Can be an absolute or relative path.</param>
        /// <returns><c>true</c> if the directory specified by <paramref name="path"/> exists; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown by implementations if <paramref name="path"/> is <c>null</c>.</exception>
        bool DirectoryExists(string path);

        /// <summary>
        /// Gets the current working directory for the executing process.
        /// </summary>
        /// <returns>The full path of the current working directory. Implementations should not return <c>null</c>.</returns>
        string GetCurrentDirectory();

        /// <summary>
        /// Sets the current working directory for the executing process.
        /// </summary>
        /// <param name="path">The path to set as the current directory. Must refer to an existing directory.</param>
        /// <exception cref="System.ArgumentNullException">Thrown by implementations if <paramref name="path"/> is <c>null</c>.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">May be thrown by implementations if the specified directory does not exist.</exception>
        void SetCurrentDirectory(string path);
    }
}
