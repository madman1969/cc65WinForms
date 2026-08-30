using System;
using System.Collections.Generic;
using System.Linq;
using cc65Wrapper.Abstractions;

namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Main error parser that delegates to specific line parsers
    /// </summary>
    public class ErrorParser : IErrorParser
    {
        private readonly IEnumerable<IErrorLineParser> _parsers;

        /// <summary>
        /// Initializes a new instance with the specified line parsers
        /// </summary>
        public ErrorParser(IEnumerable<IErrorLineParser> parsers)
        {
            _parsers = parsers?.OrderByDescending(p => p.Priority) 
                ?? throw new ArgumentNullException(nameof(parsers));
        }

        /// <summary>
        /// Parses error output into a list of errors
        /// </summary>
        public List<Cc65Error> Parse(string errorOutput)
        {
            if (string.IsNullOrWhiteSpace(errorOutput))
                return new List<Cc65Error>();

            var errors = new List<Cc65Error>();

            var lines = errorOutput.Split(
                new[] { Environment.NewLine, "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            ).Distinct();

            foreach (var line in lines)
            {
                var parser = _parsers.FirstOrDefault(p => p.CanParse(line));
                if (parser != null)
                {
                    try
                    {
                        errors.Add(parser.Parse(line));
                    }
                    catch
                    {
                        // If parsing fails, skip this line
                        continue;
                    }
                }
            }

            return errors;
        }
    }
}
