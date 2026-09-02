using cc65Wrapper.Extensions;
using cc65Wrapper.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Windows.Forms;

namespace cc65WinForms
{
    /// <summary>
    /// Application entry point and global composition root.
    /// </summary>
    /// <remarks>
    /// This static class is responsible for configuring dependency injection and logging,
    /// creating the application <see cref="IServiceProvider"/>, and starting the WinForms
    /// message loop. It is executed on the main STA thread.
    /// </remarks>
    internal static class Program
    {
        /// <summary>
        /// The application's root <see cref="IServiceProvider"/> instance.
        /// </summary>
        /// <remarks>
        /// Services are registered in <see cref="Main"/> via a <see cref="ServiceCollection"/>.
        /// This property is populated after building the service provider and is exposed as
        /// read-only for global access in places that need the DI container.
        /// </remarks>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <remarks>
        /// Behavior performed in order:
        /// - Create a <see cref="ServiceCollection"/> and register logging providers.
        ///   * Adds debug output logging (visible in Visual Studio: View > Output > Debug).
        ///   * Adds file logging to "logs/app.log", resolved relative to the executable's
        ///     directory (<see cref="AppContext.BaseDirectory"/>) rather than the process's
        ///     current working directory.
        ///   * Sets minimum log level to <see cref="LogLevel.Debug"/> in DEBUG builds,
        ///     otherwise <see cref="LogLevel.Information"/>.
        /// - Ensures the log directory exists.
        /// - Registers cc65Wrapper services via <c>services.AddCc65Wrapper()</c>.
        /// - Builds the <see cref="ServiceProvider"/> and configures a static logger factory
        ///   through <c>Cc65LoggerFactory.SetLoggerFactory</c> for backward compatibility.
        /// - Wires <see cref="Application.ThreadException"/> and
        ///   <see cref="AppDomain.UnhandledException"/> to the startup logger so exceptions
        ///   that would otherwise be lost are recorded.
        /// - Initializes WinForms visual styles and starts the main form message loop.
        /// - Disposes the service provider (flushing the file logger) once the message loop
        ///   exits, even if an exception propagates out of <see cref="Application.Run(Form)"/>.
        ///
        /// Notes:
        /// - Marked with <see cref="STAThreadAttribute"/> because WinForms requires STA.
        /// - Logging configuration and file path may be adjusted to meet deployment needs.
        /// </remarks>
        [STAThread]
        static void Main()
        {
            // Resolve log paths relative to the executable, not the process's current directory
            var logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
            var logFilePath = Path.Combine(logDirectory, "app.log");

            // Configure services and logging
            var services = new ServiceCollection();

            // Add logging - outputs to Debug window (View > Output > Debug)
            services.AddLogging(builder =>
            {
                builder.AddDebug();               // Logs to Visual Studio Debug Output
                builder.AddFile(logFilePath);     // Log to a file (creates logs/app.log by default)

#if DEBUG
                builder.SetMinimumLevel(LogLevel.Debug); // Show all logs in debug mode
#else
                builder.SetMinimumLevel(LogLevel.Information); // Only important logs in release
#endif
            });

            Directory.CreateDirectory(logDirectory);

            // Add cc65Wrapper services with logging enabled
            services.AddCc65Wrapper();

            // Build service provider
            var serviceProvider = services.BuildServiceProvider();
            ServiceProvider = serviceProvider;

            // Configure the static logger factory for backward compatibility
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            Cc65LoggerFactory.SetLoggerFactory(loggerFactory);

            var logger = loggerFactory.CreateLogger("Startup");

            // Route otherwise-unhandled exceptions to the log instead of losing them
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) =>
                logger.LogError(e.Exception, "Unhandled exception on the UI thread");
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                logger.LogCritical(e.ExceptionObject as Exception, "Unhandled exception outside the UI thread (IsTerminating: {IsTerminating})", e.IsTerminating);

            logger.LogInformation("Application starting");

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            finally
            {
                logger.LogInformation("Application exiting");
                serviceProvider.Dispose();
            }
        }
    }
}
