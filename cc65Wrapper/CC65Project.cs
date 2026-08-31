using cc65Wrapper.Enumerations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace cc65Wrapper
{
    /// <summary>
    /// Represents a serializable cc65 project definition.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class models the elements required to build a cc65 project: input and header files,
    /// target platform, optimisation flag and the output filename. Instances are JSON-serializable
    /// via <see cref="AsJson"/> and can be restored with <see cref="FromJson(string)"/>.
    /// </para>
    /// <para>
    /// The class is lightweight and intended to be used as a data transfer object between the
    /// UI and the build/runner components of the application.
    /// </para>
    /// </remarks>
    public class CC65Project
    {
        #region Constants

        /// <summary>
        /// Default on-disk/project file format version. Increment when breaking changes to the
        /// persisted JSON schema are introduced.
        /// </summary>
        public const int VERSION = 1000;

        #endregion

        #region Fields and properties

        /// <summary>Gets or sets the name of the project.</summary>
        /// <value>Human-readable project name used by the UI.</value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the working directory for the project.
        /// </summary>
        /// <value>
        /// Path to the directory which contains the project's source files. This path is used
        /// as the base for <see cref="FullOutputFilePath"/>.
        /// </value>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets the target platform for the cc65 toolchain.
        /// </summary>
        /// <value>
        /// A <see cref="CC65ProjectTypes"/> value indicating the target system (for example
        /// <c>pet</c>, <c>c64</c>, etc.).
        /// </value>
        public CC65ProjectTypes TargetPlatform { get; set; }

        /// <summary>
        /// Gets or sets the list of input source files for the project.
        /// </summary>
        /// <value>
        /// Collection of file paths (relative to <see cref="WorkingDirectory"/> or absolute)
        /// representing the project's source files. The list is kept sorted when deserialized.
        /// </value>
        public List<string> InputFiles { get; set; }

        /// <summary>
        /// Gets or sets the list of header/include files used by the project.
        /// </summary>
        /// <value>
        /// Collection of header file paths. Like <see cref="InputFiles"/>, this list is sorted
        /// on deserialization to provide consistent ordering for display and diffing.
        /// </value>
        public List<string> HeaderFiles { get; set; }

        /// <summary>
        /// Gets or sets the output filename produced by the build (for example, a ROM or binary).
        /// </summary>
        /// <value>Filename (not necessarily full path) of the generated output.</value>
        public string OutputFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether code optimisation is enabled for the build.
        /// </summary>
        /// <value><c>true</c> to enable optimisation; otherwise, <c>false</c>.</value>
        public bool OptimiseCode { get; set; }

        /// <summary>
        /// Gets or sets the persisted project format version.
        /// </summary>
        /// <value>Integer representing the stored project schema version.</value>
        public int Version { get; set; }

        /// <summary>
        /// Gets the combined path of <see cref="WorkingDirectory"/> and <see cref="OutputFile"/>.
        /// </summary>
        /// <value>
        /// The full output file path constructed with <see cref="Path.Combine(string, string)"/>.
        /// If <see cref="WorkingDirectory"/> or <see cref="OutputFile"/> is empty, the result
        /// will be a valid combination (may be just the filename).
        /// </value>
        public string FullOutputFilePath => Path.Combine(WorkingDirectory, OutputFile);

        /// <summary>
        /// Specifies if the project has been modified since it was loaded or last saved.
        /// </summary>
        /// <value><c>true</c> when the project has unsaved changes; otherwise <c>false</c>.</value>
        public bool IsModified { get; set; }

        #endregion

        #region Class Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CC65Project"/> class with sensible defaults.
        /// </summary>
        /// <remarks>
        /// Defaults:
        /// - <see cref="WorkingDirectory"/> and <see cref="OutputFile"/> are empty strings.
        /// - <see cref="TargetPlatform"/> defaults to <c>CC65ProjectTypes.pet</c>.
        /// - <see cref="InputFiles"/> and <see cref="HeaderFiles"/> are empty, initialized lists.
        /// - <see cref="OptimiseCode"/> is <c>false</c>.
        /// - <see cref="Version"/> is set to <see cref="VERSION"/>.
        /// - <see cref="IsModified"/> is <c>false</c>.
        /// </remarks>
        public CC65Project()
        {
            WorkingDirectory = string.Empty;
            TargetPlatform = CC65ProjectTypes.pet;
            HeaderFiles = new List<string>();
            InputFiles = new List<string>();
            OutputFile = string.Empty;
            OptimiseCode = false;
            Version = VERSION;
            IsModified = false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Serializes the project to an indented JSON string.
        /// </summary>
        /// <returns>A JSON string representing the current project state.</returns>
        public string AsJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Deserializes the JSON representation of a project into a <see cref="CC65Project"/> instance.
        /// </summary>
        /// <param name="Json">JSON string previously produced by <see cref="AsJson"/>.</param>
        /// <returns>A populated <see cref="CC65Project"/> instance.</returns>
        /// <remarks>
        /// After deserialization the <see cref="InputFiles"/> and <see cref="HeaderFiles"/>
        /// collections are sorted to provide deterministic ordering.
        /// </remarks>
        public static CC65Project FromJson(string Json)
        {
            CC65Project result = JsonConvert.DeserializeObject<CC65Project>(Json);

            result.InputFiles.Sort();
            result.HeaderFiles.Sort();

            return result;
        }

        #endregion
    }
}
