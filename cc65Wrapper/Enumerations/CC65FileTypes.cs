using System;

namespace cc65Wrapper.Enumerations
{
    /// <summary>
    /// A set of enumerations representing the supported file types
    /// </summary>
    [Flags]
    public enum CC65FileTypes
    {
        /// <summary>
        /// The default type enumeration
        /// </summary>
        None = 0,

        /// <summary>
        /// The source file type enumeration
        /// </summary>
        SourceFile = 2,

        /// <summary>
        /// The include file type enumeration
        /// </summary>
        IncludeFile = 4
    }
}
