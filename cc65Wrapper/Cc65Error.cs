namespace cc65Wrapper
{
    /// <summary>
    /// Represents an individual compilation raised by CL65
    /// </summary>
    public struct Cc65Error
    {
        /// <summary>
        /// Gets or sets the filename of the file containing the compilation error.
        /// </summary>
        /// <value>
        /// A <c>string</c> representing the filename the error occurred in.
        /// </value>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the line number of the compilation error.
        /// </summary>
        /// <value>
        /// An <c>int</c> value representing the line number the error occurred on.
        /// </value>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the compilation error.
        /// </summary>
        /// <value>
        /// A <c>string</c> describing the error type. One of the following: <c>Warning</c>, <c>Error</c> or <c>Fatal</c>
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the text describing the compilation error.
        /// </summary>
        /// <value>
        /// A <c>string</c> describing the compilation error encountered
        /// </value>
        public string Error { get; set; }
    }
}
