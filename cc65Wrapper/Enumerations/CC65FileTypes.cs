using System;

namespace cc65Wrapper.Enumerations
{
    /// <summary>
    /// Flags enumeration that represents the types of files handled by the cc65 toolchain wrapper.
    /// </summary>
    /// <remarks>
    /// Each member is defined as a bitmask so multiple file types can be combined using bitwise
    /// operations (for example <c>CC65FileTypes.SourceFile | CC65FileTypes.IncludeFile</c>).
    /// Typical usages:
    /// - <c>SourceFile</c> for source code files (C, assembly, etc.)
    /// - <c>IncludeFile</c> for header/include files
    /// - <c>None</c> indicates no file type selected
    /// </remarks>
    /// <example>
    /// Example: check whether a combined value includes <c>SourceFile</c>:
    /// <code>
    /// var combined = CC65FileTypes.SourceFile | CC65FileTypes.IncludeFile;
    /// bool hasSource = (combined & CC65FileTypes.SourceFile) == CC65FileTypes.SourceFile;
    /// </code>
    /// </example>
    [Flags]
    public enum CC65FileTypes
    {
        /// <summary>
        /// No file type specified. This is the default value (0) and indicates that
        /// no file-type flags are set.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents a source file (for example: C or assembly source understood by cc65).
        /// This value is a bit flag and can be combined with other file-type flags.
        /// </summary>
        SourceFile = 2,

        /// <summary>
        /// Represents an include/header file (for example: C headers).
        /// This value is a bit flag and can be combined with other file-type flags.
        /// </summary>
        IncludeFile = 4
    }
}
