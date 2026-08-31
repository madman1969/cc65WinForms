using System;
using System.ComponentModel;

namespace cc65Wrapper
{
    /// <summary>
    /// Represents the runtime configuration for a CC65 toolchain installation.
    /// </summary>
    /// <remarks>
    /// This class provides properties that map to the commonly used CC65-related
    /// environment variables. Constructing an instance will attempt to read the
    /// current environment variables and populate the properties. Call
    /// <see cref="SaveConfiguration"/> to persist any changes back to the
    /// environment variables.
    /// </remarks>
    public class Cc65CompilerConfiguration
    {
        #region Constants

        /// <summary>
        /// Environment variable name for the CC65 installation root.
        /// </summary>
        const string CC65_HOME = "CC65_HOME";

        /// <summary>
        /// Environment variable name for the CC65 include files folder.
        /// </summary>
        const string CC65_INC = "CC65_INC";

        /// <summary>
        /// Environment variable name for the ld65 configuration file path.
        /// </summary>
        const string LD65_CFG = "LD65_CFG";

        /// <summary>
        /// Environment variable name for the ld65 libraries folder.
        /// </summary>
        const string LD65_LIB = "LD65_LIB";

        /// <summary>
        /// Environment variable name pointing to the make binary location.
        /// </summary>
        const string MAKE_HOME = "MAKE_HOME";

        #endregion

        #region Fields and properties

        /// <summary>
        /// Gets or sets the CC65 installation root path.
        /// </summary>
        /// <remarks>
        /// This property maps to the <c>CC65_HOME</c> environment variable.
        /// When a new instance is created, the constructor will attempt to read
        /// the value from the environment.
        /// </remarks>
        [DisplayName("CC65_HOME")]
        [Description("The CC65_HOME Path. Root of the CC65 installation")]
        public string Cc65Home { get; set; }

        /// <summary>
        /// Gets or sets the path to the CC65 include files folder.
        /// </summary>
        /// <remarks>
        /// This property maps to the <c>CC65_INC</c> environment variable.
        /// </remarks>
        [DisplayName("CC65_INC")]
        [Description("The CC65_INC Path. The path to the CC65 include files folder")]
        public string Cc65Include { get; set; }

        /// <summary>
        /// Gets or sets the ld65 configuration file path.
        /// </summary>
        /// <remarks>
        /// This property maps to the <c>LD65_CFG</c> environment variable.
        /// </remarks>
        [DisplayName("LD65_CFG")]
        [Description("The LD65_CFG Path")]
        public string Ld65Cfg { get; set; }

        /// <summary>
        /// Gets or sets the path to the ld65 libraries folder.
        /// </summary>
        /// <remarks>
        /// This property maps to the <c>LD65_LIB</c> environment variable.
        /// </remarks>
        [DisplayName("LD65_LIB")]
        [Description("The LD65_LIB Path. The path to the CC65 library files folder")]
        public string Ld65Lib { get; set; }

        /// <summary>
        /// Gets or sets the location of the make binary.
        /// </summary>
        /// <remarks>
        /// This property maps to the <c>MAKE_HOME</c> environment variable.
        /// </remarks>
        [DisplayName("MAKE_HOME")]
        [Description("The MAKE_HOME Path. The location of the MAKE binary")]
        public string MakeHome { get; set; }

        #endregion

        #region Class Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Cc65CompilerConfiguration"/> class.
        /// </summary>
        /// <remarks>
        /// The constructor reads the current process environment variables and
        /// populates the corresponding properties. If an environment variable is
        /// not present, the related property is set to an empty string.
        /// </remarks>
        public Cc65CompilerConfiguration()
        {
            // Try to read env vars ...
            System.Collections.IDictionary envVars = Environment.GetEnvironmentVariables();

            // Use env var settings, if present ...
            Cc65Home = envVars.Contains(CC65_HOME) ? envVars[CC65_HOME].ToString() : string.Empty;
            Cc65Include = envVars.Contains(CC65_INC) ? envVars[CC65_INC].ToString() : string.Empty;
            Ld65Cfg = envVars.Contains(LD65_CFG) ? envVars[LD65_CFG].ToString() : string.Empty;
            Ld65Lib = envVars.Contains(LD65_LIB) ? envVars[LD65_LIB].ToString() : string.Empty;
            MakeHome = envVars.Contains(MAKE_HOME) ? envVars[MAKE_HOME].ToString() : string.Empty;
        }

        #endregion

        /// <summary>
        /// Saves the current CC65 configuration back to the environment variables.
        /// </summary>
        /// <returns>
        /// <c>true</c> if all environment variables were set successfully; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method updates the process-level environment variables for:
        /// <c>CC65_HOME</c>, <c>CC65_INC</c>, <c>LD65_CFG</c>, <c>LD65_LIB</c> and <c>MAKE_HOME</c>.
        /// Any exceptions encountered when attempting to write the environment are
        /// caught and written to the debug output; no exception is propagated.
        /// </remarks>
        public bool SaveConfiguration()
        {
            var result = false;

            try
            {
                // Try to save the settings ...
                Environment.SetEnvironmentVariable(CC65_HOME, Cc65Home);
                Environment.SetEnvironmentVariable(CC65_INC, Cc65Include);
                Environment.SetEnvironmentVariable(LD65_CFG, Ld65Cfg);
                Environment.SetEnvironmentVariable(LD65_LIB, Ld65Lib);
                Environment.SetEnvironmentVariable(MAKE_HOME, MakeHome);

                // If got here then must be successful ...
                result = true;
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                System.Diagnostics.Debug.WriteLine($"Failed to save CC65 configuration: {ex.Message}");
            }

            return result;
        }
    }
}
