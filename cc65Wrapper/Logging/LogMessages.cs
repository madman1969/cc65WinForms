using Microsoft.Extensions.Logging;

namespace cc65Wrapper.Logging
{
    /// <summary>
    /// High-performance logging messages implemented with the source-generator-based
    /// LoggerMessage pattern. Each partial method corresponds to a strongly-typed,
    /// allocation-free log call generated at compile time.
    /// </summary>
    internal static partial class LogMessages
    {
        // Compilation logging

        /// <summary>
        /// Logs the start of a compilation run.
        /// EventId: 1000, Level: Information
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="projectName">Name of the project being compiled.</param>
        /// <param name="targetPlatform">Target platform for the compilation.</param>
        [LoggerMessage(
            EventId = 1000,
            Level = LogLevel.Information,
            Message = "Starting compilation for project '{ProjectName}' targeting platform '{TargetPlatform}'")]
        public static partial void LogCompilationStarted(this ILogger logger, string projectName, string targetPlatform);

        /// <summary>
        /// Logs that a compilation finished successfully.
        /// EventId: 1001, Level: Information
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="duration">Duration of the compilation in milliseconds.</param>
        [LoggerMessage(
            EventId = 1001,
            Level = LogLevel.Information,
            Message = "Compilation completed successfully in {Duration}ms")]
        public static partial void LogCompilationSucceeded(this ILogger logger, long duration);

        /// <summary>
        /// Logs that a compilation failed and reports the error count.
        /// EventId: 1002, Level: Error
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="errorCount">Number of errors encountered.</param>
        /// <param name="duration">Duration of the compilation in milliseconds.</param>
        [LoggerMessage(
            EventId = 1002,
            Level = LogLevel.Error,
            Message = "Compilation failed with {ErrorCount} error(s) in {Duration}ms")]
        public static partial void LogCompilationFailed(this ILogger logger, int errorCount, long duration);

        /// <summary>
        /// Logs the exact compiler command that will be executed.
        /// EventId: 1003, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="executable">Path to the compiler executable.</param>
        /// <param name="arguments">Arguments passed to the compiler.</param>
        [LoggerMessage(
            EventId = 1003,
            Level = LogLevel.Debug,
            Message = "Compiler command: {Executable} {Arguments}")]
        public static partial void LogCompilerCommand(this ILogger logger, string executable, string arguments);

        /// <summary>
        /// Logs a compilation warning with file and line context.
        /// EventId: 1004, Level: Warning
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="fileName">File where the warning occurred.</param>
        /// <param name="lineNumber">Line number of the warning.</param>
        /// <param name="message">Warning message text.</param>
        [LoggerMessage(
            EventId = 1004,
            Level = LogLevel.Warning,
            Message = "Compilation warning at {FileName}:{LineNumber} - {Message}")]
        public static partial void LogCompilationWarning(this ILogger logger, string fileName, int lineNumber, string message);

        /// <summary>
        /// Logs a compilation error with file and line context.
        /// EventId: 1005, Level: Error
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="fileName">File where the error occurred.</param>
        /// <param name="lineNumber">Line number of the error.</param>
        /// <param name="message">Error message text.</param>
        [LoggerMessage(
            EventId = 1005,
            Level = LogLevel.Error,
            Message = "Compilation error at {FileName}:{LineNumber} - {Message}")]
        public static partial void LogCompilationError(this ILogger logger, string fileName, int lineNumber, string message);

        /// <summary>
        /// Logs changes in the build phase.
        /// EventId: 1006, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="phase">Name of the new build phase.</param>
        /// <param name="message">Optional message describing the phase change.</param>
        [LoggerMessage(
            EventId = 1006,
            Level = LogLevel.Debug,
            Message = "Build phase changed: {Phase} - {Message}")]
        public static partial void LogBuildPhase(this ILogger logger, string phase, string message);

        // Emulator logging

        /// <summary>
        /// Logs starting of the emulator process.
        /// EventId: 2000, Level: Information
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="emulatorPath">Path to the emulator executable.</param>
        /// <param name="targetPlatform">Target platform the emulator will run.</param>
        [LoggerMessage(
            EventId = 2000,
            Level = LogLevel.Information,
            Message = "Starting emulator '{EmulatorPath}' for platform '{TargetPlatform}'")]
        public static partial void LogEmulatorStarted(this ILogger logger, string emulatorPath, string targetPlatform);

        /// <summary>
        /// Logs successful emulator launch with process id.
        /// EventId: 2001, Level: Information
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="processId">Process ID of the launched emulator.</param>
        [LoggerMessage(
            EventId = 2001,
            Level = LogLevel.Information,
            Message = "Emulator launched successfully with PID {ProcessId}")]
        public static partial void LogEmulatorLaunched(this ILogger logger, int processId);

        /// <summary>
        /// Logs failure to launch the emulator.
        /// EventId: 2002, Level: Error
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="errorMessage">Error message describing the launch failure.</param>
        [LoggerMessage(
            EventId = 2002,
            Level = LogLevel.Error,
            Message = "Failed to launch emulator: {ErrorMessage}")]
        public static partial void LogEmulatorLaunchFailed(this ILogger logger, string errorMessage);

        /// <summary>
        /// Logs the emulator command that will be executed.
        /// EventId: 2003, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="executable">Emulator executable path.</param>
        /// <param name="arguments">Arguments passed to the emulator.</param>
        [LoggerMessage(
            EventId = 2003,
            Level = LogLevel.Debug,
            Message = "Emulator command: {Executable} {Arguments}")]
        public static partial void LogEmulatorCommand(this ILogger logger, string executable, string arguments);

        // Command execution logging

        /// <summary>
        /// Logs that an external command is being executed.
        /// EventId: 3000, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="executable">Command executable path.</param>
        /// <param name="arguments">Command arguments.</param>
        [LoggerMessage(
            EventId = 3000,
            Level = LogLevel.Debug,
            Message = "Executing command: {Executable} {Arguments}")]
        public static partial void LogCommandExecuting(this ILogger logger, string executable, string arguments);

        /// <summary>
        /// Logs completion of an external command.
        /// EventId: 3001, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="exitCode">Exit code returned by the command.</param>
        [LoggerMessage(
            EventId = 3001,
            Level = LogLevel.Debug,
            Message = "Command completed with exit code {ExitCode}")]
        public static partial void LogCommandCompleted(this ILogger logger, int exitCode);

        /// <summary>
        /// Logs a failed command execution including captured error output.
        /// EventId: 3002, Level: Error
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="exitCode">Exit code returned by the failed command.</param>
        /// <param name="errorOutput">Standard error output captured from the process.</param>
        [LoggerMessage(
            EventId = 3002,
            Level = LogLevel.Error,
            Message = "Command execution failed with exit code {ExitCode}: {ErrorOutput}")]
        public static partial void LogCommandFailed(this ILogger logger, int exitCode, string errorOutput);

        // Error parsing logging

        /// <summary>
        /// Logs the start of multi-line error parsing.
        /// EventId: 4000, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="lineCount">Number of lines to parse.</param>
        /// <param name="parserCount">Number of parsers used.</param>
        [LoggerMessage(
            EventId = 4000,
            Level = LogLevel.Debug,
            Message = "Parsing {LineCount} error lines using {ParserCount} parser(s)")]
        public static partial void LogErrorParsingStarted(this ILogger logger, int lineCount, int parserCount);

        /// <summary>
        /// Logs that a specific parser handled an error line.
        /// EventId: 4001, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="parserName">Name of the parser used.</param>
        /// <param name="line">The error line that was parsed.</param>
        [LoggerMessage(
            EventId = 4001,
            Level = LogLevel.Debug,
            Message = "Parser '{ParserName}' handled error line: {Line}")]
        public static partial void LogErrorLineParsed(this ILogger logger, string parserName, string line);

        /// <summary>
        /// Logs when no parser can handle an error line.
        /// EventId: 4002, Level: Warning
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="line">The unparsed error line.</param>
        [LoggerMessage(
            EventId = 4002,
            Level = LogLevel.Warning,
            Message = "No parser could handle error line: {Line}")]
        public static partial void LogErrorLineNotParsed(this ILogger logger, string line);

        /// <summary>
        /// Logs the result of error parsing with counts for errors and warnings found.
        /// EventId: 4003, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="errorCount">Number of errors found.</param>
        /// <param name="warningCount">Number of warnings found.</param>
        [LoggerMessage(
            EventId = 4003,
            Level = LogLevel.Debug,
            Message = "Error parsing completed: found {ErrorCount} error(s), {WarningCount} warning(s)")]
        public static partial void LogErrorParsingCompleted(this ILogger logger, int errorCount, int warningCount);

        // Validation logging

        /// <summary>
        /// Logs a validation failure.
        /// EventId: 5000, Level: Error
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="validationError">Description of the validation failure.</param>
        [LoggerMessage(
            EventId = 5000,
            Level = LogLevel.Error,
            Message = "Validation failed: {ValidationError}")]
        public static partial void LogValidationFailed(this ILogger logger, string validationError);

        /// <summary>
        /// Logs a validation warning.
        /// EventId: 5001, Level: Warning
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="validationWarning">Description of the validation warning.</param>
        [LoggerMessage(
            EventId = 5001,
            Level = LogLevel.Warning,
            Message = "Validation warning: {ValidationWarning}")]
        public static partial void LogValidationWarning(this ILogger logger, string validationWarning);

        // Configuration logging

        /// <summary>
        /// Logs that the CC65 environment was loaded.
        /// EventId: 6000, Level: Information
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="cc65Home">Value of CC65_HOME.</param>
        /// <param name="ld65Cfg">Value of LD65_CFG.</param>
        [LoggerMessage(
            EventId = 6000,
            Level = LogLevel.Information,
            Message = "CC65 environment loaded: CC65_HOME='{Cc65Home}', LD65_CFG='{Ld65Cfg}'")]
        public static partial void LogConfigurationLoaded(this ILogger logger, string cc65Home, string ld65Cfg);

        /// <summary>
        /// Logs a missing CC65-related environment variable.
        /// EventId: 6001, Level: Warning
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="variableName">Name of the missing environment variable.</param>
        [LoggerMessage(
            EventId = 6001,
            Level = LogLevel.Warning,
            Message = "CC65 environment variable '{VariableName}' is not set")]
        public static partial void LogConfigurationMissing(this ILogger logger, string variableName);

        /// <summary>
        /// Logs that configuration was saved successfully.
        /// EventId: 6002, Level: Information
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        [LoggerMessage(
            EventId = 6002,
            Level = LogLevel.Information,
            Message = "Configuration saved successfully")]
        public static partial void LogConfigurationSaved(this ILogger logger);

        // File system logging

        /// <summary>
        /// Logs when the working directory is changed.
        /// EventId: 7000, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="directory">Directory that was switched to.</param>
        [LoggerMessage(
            EventId = 7000,
            Level = LogLevel.Debug,
            Message = "Working directory changed to: {Directory}")]
        public static partial void LogWorkingDirectoryChanged(this ILogger logger, string directory);

        /// <summary>
        /// Logs when the working directory is restored.
        /// EventId: 7001, Level: Debug
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="directory">Directory restored to.</param>
        [LoggerMessage(
            EventId = 7001,
            Level = LogLevel.Debug,
            Message = "Working directory restored to: {Directory}")]
        public static partial void LogWorkingDirectoryRestored(this ILogger logger, string directory);

        /// <summary>
        /// Logs a missing file error.
        /// EventId: 7002, Level: Error
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="filePath">Path of the missing file.</param>
        [LoggerMessage(
            EventId = 7002,
            Level = LogLevel.Error,
            Message = "File not found: {FilePath}")]
        public static partial void LogFileNotFound(this ILogger logger, string filePath);

        /// <summary>
        /// Logs a missing directory error.
        /// EventId: 7003, Level: Error
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="directoryPath">Path of the missing directory.</param>
        [LoggerMessage(
            EventId = 7003,
            Level = LogLevel.Error,
            Message = "Directory not found: {DirectoryPath}")]
        public static partial void LogDirectoryNotFound(this ILogger logger, string directoryPath);
    }
}
