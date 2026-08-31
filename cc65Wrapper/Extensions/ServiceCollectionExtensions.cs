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
    /// Provides extension methods to register the cc65Wrapper components into an
    /// <see cref="IServiceCollection"/> for dependency injection.
    /// </summary>
    /// <remarks>
    /// This class centralizes registration of core abstractions, argument builders,
    /// error parsing implementations and the primary services (<see cref="ICompiler"/> and
    /// <see cref="IEmulatorLauncher"/>). All registrations use singleton lifetime because
    /// the wrapped operations are stateless and shared instances simplify lifetime management
    /// in typical desktop and CLI applications.
    /// </remarks>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all cc65Wrapper services to the provided <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The service collection to which cc65Wrapper services will be added.
        /// This parameter must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// The same <see cref="IServiceCollection"/> instance to enable call chaining.
        /// </returns>
        /// <remarks>
        /// Registered services:
        /// - Core abstractions: <see cref="ICommandExecutor"/>, <see cref="IFileSystem"/>.
        /// - Argument builders for domain models (e.g. <see cref="CC65Project"/>, <see cref="EmulatorLaunchOptions"/>).
        /// - Error line parsers (registered in priority order) and a composite <see cref="IErrorParser"/>.
        /// - Main services: <see cref="ICompiler"/> and <see cref="IEmulatorLauncher"/>.
        ///
        /// Note on error parsers: multiple implementations of <see cref="IErrorLineParser"/>
        /// are registered. They are expected to be consumed in the registered order so that
        /// more specific parsers can be attempted before falling back to the <see cref="DefaultErrorParser"/>.
        /// </remarks>
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
        /// Adds cc65Wrapper services and configures logging using the provided delegate.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="configureLogging">
        /// A delegate used to configure logging providers and filters via an <see cref="ILoggingBuilder"/>.
        /// </param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        /// <remarks>
        /// This overload registers logging first so that services resolved from the container
        /// can obtain an <see cref="ILogger{T}"/> with the requested configuration.
        /// </remarks>
        public static IServiceCollection AddCc65Wrapper(
            this IServiceCollection services,
            System.Action<ILoggingBuilder> configureLogging)
        {
            services.AddLogging(configureLogging);
            return services.AddCc65Wrapper();
        }
    }
}
