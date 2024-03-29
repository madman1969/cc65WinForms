﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace cc65Wrapper
{
    /// <summary>
    /// Class defining the elements of a project
    /// </summary>
    public class CC65Project
    {
        #region Constants

        public const int VERSION = 1000;

        #endregion

        #region Fields and properties

        /// <summary>Gets or sets the name of the project.</summary>
        /// <value>The name of the project.</value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the working directory.
        /// </summary>
        /// <value>
        /// The working directory.
        /// </value>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets the target platform.
        /// </summary>
        /// <value>
        /// The target platform.
        /// </value>
        public string TargetPlatform { get; set; }

        /// <summary>
        /// Gets or sets the input files.
        /// </summary>
        /// <value>
        /// The input files.
        /// </value>
        public List<string> InputFiles { get; set; }

        /// <summary>
        /// Gets or sets the header files.
        /// </summary>
        /// <value>
        /// The header files.
        /// </value>
        public List<string> HeaderFiles { get; set; }

        /// <summary>
        /// Gets or sets the output file.
        /// </summary>
        /// <value>
        /// The output file.
        /// </value>
        public string OutputFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [optimise code].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [optimise code]; otherwise, <c>false</c>.
        /// </value>
        public bool OptimiseCode { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; set; }

        /// <summary>
        /// Gets the full output file path.
        /// </summary>
        /// <value>
        /// The full output file path.
        /// </value>
        public string FullOutputFilePath => Path.Combine(WorkingDirectory, OutputFile);

        /// <summary>
        /// Specifies if the project has been modified since it was loaded
        /// </summary>
        /// <value>
        /// A boolean value
        /// </value>
        public bool IsModified { get; set; }

        #endregion

        #region Class Constructor

        /// <summary>
        /// Class Constructor
        /// </summary>
        public CC65Project()
        {
            WorkingDirectory = string.Empty;
            TargetPlatform = "pet";
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
        /// Retrieves JSON representation of a project
        /// </summary>
        /// <returns>A JSON <c>string</c> representation of a <c>Cc65Project</c> instance</returns>
        public string AsJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Converts JSON <c>string</c> representation of a <c>Cc65Project</c> into a <c>Cc65Project</c> instance
        /// </summary>
        /// <param name="Json">A JSON format <c>string</c> representation of a <c>Cc65Project</c></param>
        /// <returns>A populated <c>Cc65Project</c> instance</returns>
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
