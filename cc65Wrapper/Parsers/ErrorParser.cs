using System;
using System.Collections.Generic;
using System.Linq;
using cc65Wrapper.Abstractions;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Logging;

namespace cc65Wrapper.Parsers
{
    /// <summary>
    /// Main error parser that delegates individual error lines to a set of
    /// <see cref="IErrorLineParser"/> implementations.
    /// 
    /// The parser:
    /// - Orders provided line parsers by their <c>Priority</c> (descending) so higher
    ///   priority parsers are attempted first.
    /// - Splits the raw error output into distinct non-empty lines and attempts to parse
    ///   each line using the first parser whose <see cref="IErrorLineParser.CanParse(string)"/>
    ///   returns true.
    /// - Collects and returns the resulting <see cref="Cc65Error"/> instances.
    /// - Emits structured log entries for start, per-line success/failure, and completion.
    /// </summary>
    public class ErrorParser : IErrorParser
    {
        /// <summary>
        /// The collection of line parsers used to parse individual lines. Parsers are
        /// ordered by <c>Priority</c> descending at construction time.
        /// </summary>
        private readonly IEnumerable<IErrorLineParser> _parsers;

        /// <summary>
        /// Logger used for diagnostic logging of parsing progress and failures.
        /// </summary>
        private readonly ILogger<ErrorParser> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorParser"/>.
        /// </summary>
        /// <param name="parsers">
        /// A non-null collection of <see cref="IErrorLineParser"/> instances used to
        /// parse individual error output lines. Parsers are ordered by <c>Priority</c>
        /// descending; higher priority parsers are tried before lower priority ones.
        /// </param>
        /// <param name="logger">
        /// Optional logger. If null, a default logger is obtained from
        /// <see cref="Cc65LoggerFactory.CreateLogger{T}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="parsers"/> is null.
        /// </exception>
        public ErrorParser(IEnumerable<IErrorLineParser> parsers, ILogger<ErrorParser> logger = null)
        {
            _parsers = parsers?.OrderByDescending(p => p.Priority) 
                ?? throw new ArgumentNullException(nameof(parsers));
            _logger = logger ?? Cc65LoggerFactory.CreateLogger<ErrorParser>();
        }

        /// <summary>
        /// Parses the provided raw error output into a list of <see cref="Cc65Error"/>.
        /// </summary>
        /// <param name="errorOutput">
        /// Raw multi-line error output from the cc65 toolchain. May contain CR, LF or CRLF line endings.
        /// </param>
        /// <returns>
        /// A list of parsed <see cref="Cc65Error"/> objects. Returns an empty list if
        /// <paramref name="errorOutput"/> is <c>null</c>, empty or whitespace-only.
        /// Duplicate lines in the input are ignored (each distinct non-empty line is parsed once).
        /// </returns>
        /// <remarks>
        /// Processing steps:
        /// 1. If <paramref name="errorOutput"/> is null/whitespace, an empty list is returned.
        /// 2. The input is split on environment newlines, CR and LF. Empty entries are removed and
        ///    distinct lines are retained.
        /// 3. For each distinct line, the parser selects the first <see cref="IErrorLineParser"/>
        ///    whose <c>CanParse</c> returns true and calls its <c>Parse</c> method.
        /// 4. Parsing exceptions for individual lines are caught and logged as warnings; parsing continues.
        /// 5. The method logs the number of errors and warnings parsed on completion.
        /// </remarks>
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
