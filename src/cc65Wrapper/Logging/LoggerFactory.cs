using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace cc65Wrapper.Logging
{
    /// <summary>
    /// Factory for creating loggers with fallback to NullLogger when not configured
    /// </summary>
    public static class Cc65LoggerFactory
    {
        private static ILoggerFactory? _loggerFactory;

        /// <summary>
        /// Sets the global <see cref="ILoggerFactory"/> used by the cc65Wrapper library.
        /// </summary>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> instance to use. This should be provided during application startup.</param>
        /// <remarks>
        /// - Replaces any previously configured factory; subsequent calls will overwrite the existing factory.
        /// - If this method is not called, the library falls back to <see cref="NullLogger{T}.Instance"/> and <see cref="NullLogger.Instance"/> when creating loggers.
        /// - This method does not perform synchronization; callers should ensure it is invoked in a thread-safe manner (typically once during initialization).
        /// </remarks>
        public static void SetLoggerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Creates a logger for the specified type
        /// </summary>
        /// <typeparam name="T">The type to create a logger for</typeparam>
        /// <returns>An ILogger instance</returns>
        public static ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory?.CreateLogger<T>() ?? NullLogger<T>.Instance;
        }

        /// <summary>
        /// Creates a logger with the specified category name
        /// </summary>
        /// <param name="categoryName">The category name for the logger</param>
        /// <returns>An ILogger instance</returns>
        public static ILogger CreateLogger(string categoryName)
        {
            return _loggerFactory?.CreateLogger(categoryName) ?? NullLogger.Instance;
        }

        /// <summary>
        /// Resets the logger factory (useful for testing)
        /// </summary>
        internal static void Reset()
        {
            _loggerFactory = null;
        }
    }
}
