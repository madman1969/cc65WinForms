using System;
using System.Collections.Generic;
using System.Linq;
using cc65Wrapper.Abstractions;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Logging;

namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Main error parser that delegates to specific line parsers
    /// </summary>
    public class ErrorParser : IErrorParser
    {
        private readonly IEnumerable<IErrorLineParser> _parsers;
        private readonly ILogger<ErrorParser> _logger;

        /// <summary>
        /// Initializes a new instance with the specified line parsers
        /// </summary>
        public ErrorParser(IEnumerable<IErrorLineParser> parsers, ILogger<ErrorParser> logger = null)
        {
            _parsers = parsers?.OrderByDescending(p => p.Priority) 
                ?? throw new ArgumentNullException(nameof(parsers));
            _logger = logger ?? Cc65LoggerFactory.CreateLogger<ErrorParser>();
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
            ).Distinct().ToArray();

            _logger.LogErrorParsingStarted(lines.Length, _parsers.Count());

            foreach (var line in lines)
            {
                var parser = _parsers.FirstOrDefault(p => p.CanParse(line));
                if (parser != null)
                {
                    try
                    {
                        var error = parser.Parse(line);
                        errors.Add(error);
                        _logger.LogErrorLineParsed(parser.GetType().Name, line);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to parse error line: {Line}", line);
                        continue;
                    }
                }
                else
                {
                    _logger.LogErrorLineNotParsed(line);
                }
            }

            var errorCount = errors.Count(e => !e.Type.Equals("Warning", StringComparison.OrdinalIgnoreCase));
            var warningCount = errors.Count(e => e.Type.Equals("Warning", StringComparison.OrdinalIgnoreCase));
            _logger.LogErrorParsingCompleted(errorCount, warningCount);

            return errors;
        }
    }
}
