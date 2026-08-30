using Microsoft.Extensions.Logging;

namespace cc65Wrapper.Logging
{
    /// <summary>
    /// High-performance logging messages using source generators (LoggerMessage pattern)
    /// </summary>
    internal static partial class LogMessages
    {
        // Compilation logging
        [LoggerMessage(
            EventId = 1000,
            Level = LogLevel.Information,
            Message = "Starting compilation for project '{ProjectName}' targeting platform '{TargetPlatform}'")]
        public static partial void LogCompilationStarted(this ILogger logger, string projectName, string targetPlatform);

        [LoggerMessage(
            EventId = 1001,
            Level = LogLevel.Information,
            Message = "Compilation completed successfully in {Duration}ms")]
        public static partial void LogCompilationSucceeded(this ILogger logger, long duration);

        [LoggerMessage(
            EventId = 1002,
            Level = LogLevel.Error,
            Message = "Compilation failed with {ErrorCount} error(s) in {Duration}ms")]
        public static partial void LogCompilationFailed(this ILogger logger, int errorCount, long duration);

        [LoggerMessage(
            EventId = 1003,
            Level = LogLevel.Debug,
            Message = "Compiler command: {Executable} {Arguments}")]
        public static partial void LogCompilerCommand(this ILogger logger, string executable, string arguments);

        [LoggerMessage(
            EventId = 1004,
            Level = LogLevel.Warning,
            Message = "Compilation warning at {FileName}:{LineNumber} - {Message}")]
        public static partial void LogCompilationWarning(this ILogger logger, string fileName, int lineNumber, string message);

        [LoggerMessage(
            EventId = 1005,
            Level = LogLevel.Error,
            Message = "Compilation error at {FileName}:{LineNumber} - {Message}")]
        public static partial void LogCompilationError(this ILogger logger, string fileName, int lineNumber, string message);

        [LoggerMessage(
            EventId = 1006,
            Level = LogLevel.Debug,
            Message = "Build phase changed: {Phase} - {Message}")]
        public static partial void LogBuildPhase(this ILogger logger, string phase, string message);

        // Emulator logging
        [LoggerMessage(
            EventId = 2000,
            Level = LogLevel.Information,
            Message = "Starting emulator '{EmulatorPath}' for platform '{TargetPlatform}'")]
        public static partial void LogEmulatorStarted(this ILogger logger, string emulatorPath, string targetPlatform);

        [LoggerMessage(
            EventId = 2001,
            Level = LogLevel.Information,
            Message = "Emulator launched successfully with PID {ProcessId}")]
        public static partial void LogEmulatorLaunched(this ILogger logger, int processId);

        [LoggerMessage(
            EventId = 2002,
            Level = LogLevel.Error,
            Message = "Failed to launch emulator: {ErrorMessage}")]
        public static partial void LogEmulatorLaunchFailed(this ILogger logger, string errorMessage);

        [LoggerMessage(
            EventId = 2003,
            Level = LogLevel.Debug,
            Message = "Emulator command: {Executable} {Arguments}")]
        public static partial void LogEmulatorCommand(this ILogger logger, string executable, string arguments);

        // Command execution logging
        [LoggerMessage(
            EventId = 3000,
            Level = LogLevel.Debug,
            Message = "Executing command: {Executable} {Arguments}")]
        public static partial void LogCommandExecuting(this ILogger logger, string executable, string arguments);

        [LoggerMessage(
            EventId = 3001,
            Level = LogLevel.Debug,
            Message = "Command completed with exit code {ExitCode}")]
        public static partial void LogCommandCompleted(this ILogger logger, int exitCode);

        [LoggerMessage(
            EventId = 3002,
            Level = LogLevel.Error,
            Message = "Command execution failed with exit code {ExitCode}: {ErrorOutput}")]
        public static partial void LogCommandFailed(this ILogger logger, int exitCode, string errorOutput);

        // Error parsing logging
        [LoggerMessage(
            EventId = 4000,
            Level = LogLevel.Debug,
            Message = "Parsing {LineCount} error lines using {ParserCount} parser(s)")]
        public static partial void LogErrorParsingStarted(this ILogger logger, int lineCount, int parserCount);

        [LoggerMessage(
            EventId = 4001,
            Level = LogLevel.Debug,
            Message = "Parser '{ParserName}' handled error line: {Line}")]
        public static partial void LogErrorLineParsed(this ILogger logger, string parserName, string line);

        [LoggerMessage(
            EventId = 4002,
            Level = LogLevel.Warning,
            Message = "No parser could handle error line: {Line}")]
        public static partial void LogErrorLineNotParsed(this ILogger logger, string line);

        [LoggerMessage(
            EventId = 4003,
            Level = LogLevel.Debug,
            Message = "Error parsing completed: found {ErrorCount} error(s), {WarningCount} warning(s)")]
        public static partial void LogErrorParsingCompleted(this ILogger logger, int errorCount, int warningCount);

        // Validation logging
        [LoggerMessage(
            EventId = 5000,
            Level = LogLevel.Error,
            Message = "Validation failed: {ValidationError}")]
        public static partial void LogValidationFailed(this ILogger logger, string validationError);

        [LoggerMessage(
            EventId = 5001,
            Level = LogLevel.Warning,
            Message = "Validation warning: {ValidationWarning}")]
        public static partial void LogValidationWarning(this ILogger logger, string validationWarning);

        // Configuration logging
        [LoggerMessage(
            EventId = 6000,
            Level = LogLevel.Information,
            Message = "CC65 environment loaded: CC65_HOME='{Cc65Home}', LD65_CFG='{Ld65Cfg}'")]
        public static partial void LogConfigurationLoaded(this ILogger logger, string cc65Home, string ld65Cfg);

        [LoggerMessage(
            EventId = 6001,
            Level = LogLevel.Warning,
            Message = "CC65 environment variable '{VariableName}' is not set")]
        public static partial void LogConfigurationMissing(this ILogger logger, string variableName);

        [LoggerMessage(
            EventId = 6002,
            Level = LogLevel.Information,
            Message = "Configuration saved successfully")]
        public static partial void LogConfigurationSaved(this ILogger logger);

        // File system logging
        [LoggerMessage(
            EventId = 7000,
            Level = LogLevel.Debug,
            Message = "Working directory changed to: {Directory}")]
        public static partial void LogWorkingDirectoryChanged(this ILogger logger, string directory);

        [LoggerMessage(
            EventId = 7001,
            Level = LogLevel.Debug,
            Message = "Working directory restored to: {Directory}")]
        public static partial void LogWorkingDirectoryRestored(this ILogger logger, string directory);

        [LoggerMessage(
            EventId = 7002,
            Level = LogLevel.Error,
            Message = "File not found: {FilePath}")]
        public static partial void LogFileNotFound(this ILogger logger, string filePath);

        [LoggerMessage(
            EventId = 7003,
            Level = LogLevel.Error,
            Message = "Directory not found: {DirectoryPath}")]
        public static partial void LogDirectoryNotFound(this ILogger logger, string directoryPath);
    }
}
