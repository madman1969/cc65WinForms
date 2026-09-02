using System;
using System.IO;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Services
{
    /// <summary>
    /// Default <see cref="IProjectService"/> implementation that reads and writes
    /// <see cref="CC65Project"/> instances as JSON files on the local disk.
    /// </summary>
    public class ProjectService : IProjectService
    {
        /// <inheritdoc />
        public CC65Project Load(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty", nameof(filePath));

            var json = File.ReadAllText(filePath);
            return CC65Project.FromJson(json);
        }

        /// <inheritdoc />
        public void Save(CC65Project project, string filePath)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty", nameof(filePath));

            File.WriteAllText(filePath, project.AsJson());
        }
    }
}
