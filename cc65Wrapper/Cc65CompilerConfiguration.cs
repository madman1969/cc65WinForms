using System;
using System.ComponentModel;

namespace cc65Wrapper
{
    /// <summary>
    /// A class representing the CC65 configuration
    /// </summary>
    public class Cc65CompilerConfiguration
    {
        #region Constants

        // A set of constants for the various CC65 environment variables ...
        const string CC65_HOME = "CC65_HOME";
        const string CC65_INC = "CC65_INC";
        const string LD65_CFG = "LD65_CFG";
        const string LD65_LIB = "LD65_LIB";
        const string MAKE_HOME = "MAKE_HOME";

        #endregion

        #region Fields and properties

        [DisplayName("CC65_HOME")]
        [Description("The CC65_HOME Path. Root of the CC65 installation")]
        public string Cc65Home { get; set; }

        [DisplayName("CC65_INC")]
        [Description("The CC65_INC Path. The path to the CC65 include files folder")]
        public string Cc65Include { get; set; }

        [DisplayName("LD65_CFG")]
        [Description("The LD65_CFG Path")]
        public string Ld65Cfg { get; set; }

        [DisplayName("LD65_LIB")]
        [Description("The LD65_LIB Path. The path to the CC65 library files folder")]
        public string Ld65Lib { get; set; }

        [DisplayName("MAKE_HOME")]
        [Description("The MAKE_HOME Path. The location of the MAKE binary")]
        public string MakeHome { get; set; }

        #endregion

        #region Class Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Cc65CompilerConfiguration"/> class.
        /// </summary>
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
        /// Saves the current CC65 configuration back to the environment variables
        /// </summary>
        /// <returns><c>true</c> if successful; else <c>false</c></returns>
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
