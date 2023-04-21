namespace cc65Wrapper
{
    /// <summary>
    /// Represents an individual compilation raised by CL65
    /// </summary>
    public struct Cc65Error
    {
        public string Filename { get; set; }
        public int LineNumber { get; set; }
        public string Error { get; set; }
    }
}
