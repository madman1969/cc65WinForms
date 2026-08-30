using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Extensions;
using cc65Wrapper.Logging;

namespace cc65WinForms
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configure services and logging
            var services = new ServiceCollection();

            // Add logging - outputs to Debug window (View > Output > Debug)
            services.AddLogging(builder =>
            {
                builder.AddDebug(); // Logs to Visual Studio Debug Output

#if DEBUG
                builder.SetMinimumLevel(LogLevel.Debug); // Show all logs in debug mode
#else
                builder.SetMinimumLevel(LogLevel.Information); // Only important logs in release
#endif
            });

            // Add cc65Wrapper services with logging enabled
            services.AddCc65Wrapper();

            // Build service provider
            ServiceProvider = services.BuildServiceProvider();

            // Configure the static logger factory for backward compatibility
            var loggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
            Cc65LoggerFactory.SetLoggerFactory(loggerFactory);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
