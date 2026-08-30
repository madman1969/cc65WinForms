using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Abstractions;
using cc65Wrapper.Builders;
using cc65Wrapper.Infrastructure;
using cc65Wrapper.Parsers;
using cc65Wrapper.Services;

namespace cc65Wrapper.Extensions
{
    /// <summary>
    /// Extension methods for configuring cc65Wrapper services in dependency injection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds cc65Wrapper services to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddCc65Wrapper(this IServiceCollection services)
        {
            // Core abstractions
            services.AddSingleton<ICommandExecutor, CliWrapCommandExecutor>();
            services.AddSingleton<IFileSystem, FileSystemWrapper>();

            // Argument builders
            services.AddSingleton<IArgumentBuilder<CC65Project>, CompilerArgumentBuilder>();
            services.AddSingleton<IArgumentBuilder<EmulatorLaunchOptions>, EmulatorArgumentBuilder>();

            // Error parsers (in priority order)
            services.AddSingleton<IErrorLineParser, FourPartErrorParser>();
            services.AddSingleton<IErrorLineParser, FivePartErrorParser>();
            services.AddSingleton<IErrorLineParser, ThreePartErrorParser>();
            services.AddSingleton<IErrorLineParser, DefaultErrorParser>();
            services.AddSingleton<IErrorParser, ErrorParser>();

            // Main services
            services.AddSingleton<ICompiler, Cc65Compiler>();
            services.AddSingleton<IEmulatorLauncher, Cc65EmulatorLauncher>();

            return services;
        }

        /// <summary>
        /// Adds cc65Wrapper services with logging configuration
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configureLogging">Action to configure logging</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddCc65Wrapper(
            this IServiceCollection services,
            System.Action<ILoggingBuilder> configureLogging)
        {
            services.AddLogging(configureLogging);
            return services.AddCc65Wrapper();
        }
    }
}
