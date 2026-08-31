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
        ///   * Adds file logging to "logs/app.log".
        ///   * Sets minimum log level to <see cref="LogLevel.Debug"/> in DEBUG builds,
        ///     otherwise <see cref="LogLevel.Information"/>.
        /// - Ensures the "logs" directory exists.
        /// - Registers cc65Wrapper services via <c>services.AddCc65Wrapper()</c>.
        /// - Builds the <see cref="ServiceProvider"/> and configures a static logger factory
        ///   through <c>Cc65LoggerFactory.SetLoggerFactory</c> for backward compatibility.
        /// - Emits a test information message to verify file logging.
        /// - Initializes WinForms visual styles and starts the main form message loop.
        ///
        /// Notes:
        /// - Marked with <see cref="STAThreadAttribute"/> because WinForms requires STA.
        /// - Logging configuration and file path may be adjusted to meet deployment needs.
        /// </remarks>
        [STAThread]
        static void Main()
        {
            // Configure services and logging
            var services = new ServiceCollection();

            // Add logging - outputs to Debug window (View > Output > Debug)
            services.AddLogging(builder =>
            {
                builder.AddDebug();                 // Logs to Visual Studio Debug Output                
                builder.AddFile("logs/app.log");    // Log to a file (creates logs/app.log by default)

#if DEBUG
                builder.SetMinimumLevel(LogLevel.Debug); // Show all logs in debug mode
#else
                builder.SetMinimumLevel(LogLevel.Information); // Only important logs in release
#endif
            });

            Directory.CreateDirectory("logs");

            // Add cc65Wrapper services with logging enabled
            services.AddCc65Wrapper();

            // Build service provider
            ServiceProvider = services.BuildServiceProvider();

            // Configure the static logger factory for backward compatibility
            var loggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
            Cc65LoggerFactory.SetLoggerFactory(loggerFactory);

            // Test logging ...
            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogInformation("File logging is working");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
