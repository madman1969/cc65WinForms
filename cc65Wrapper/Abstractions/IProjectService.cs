namespace cc65Wrapper.Abstractions
{
    /// <summary>
    /// Defines an abstraction for loading and saving <see cref="CC65Project"/> instances
    /// to and from disk as JSON.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Loads a <see cref="CC65Project"/> from the JSON file at <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">Full path to the project's JSON file.</param>
        /// <returns>The deserialized <see cref="CC65Project"/> instance.</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="filePath"/> is null or empty.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown if the file does not exist.</exception>
        CC65Project Load(string filePath);

        /// <summary>
        /// Serializes <paramref name="project"/> to JSON and writes it to <paramref name="filePath"/>,
        /// overwriting any existing file.
        /// </summary>
        /// <param name="project">The project to save.</param>
        /// <param name="filePath">Full path to write the project's JSON file to.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="project"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="filePath"/> is null or empty.</exception>
        void Save(CC65Project project, string filePath);
    }
}
