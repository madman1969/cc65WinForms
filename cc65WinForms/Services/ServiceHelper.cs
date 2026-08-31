using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Abstractions;

namespace cc65WinForms.Services
{

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
    /// <summary>
    /// Static helper to resolve dependency-injected services from WinForms code.
    /// </summary>
    /// <remarks>
    /// This class wraps calls to the application's root <see cref="IServiceProvider"/> (via <c>Program.ServiceProvider</c>)
    /// to make resolving common services convenient from places where constructor injection is not available (for example
    /// legacy WinForms event handlers). Prefer constructor injection where possible.
    ///
    /// Important:
    /// - <see cref="GetRequiredService{T}"/> is used under the hood; if a service is not registered an
    ///   <see cref="InvalidOperationException"/> will be thrown.
    /// - Do not use these helpers to resolve scoped services from the root provider; resolving scoped services from the
    ///   root provider can lead to incorrect lifetimes. Create an appropriate scope when you need scoped services.
    /// </remarks>
    public static class ServiceHelper
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
    {
        /// <summary>
        /// Gets the <see cref="ICompiler"/> service from the application's service provider.
        /// </summary>
        /// <returns>The registered <see cref="ICompiler"/> implementation.</returns>
        /// <exception cref="InvalidOperationException">Thrown if <see cref="ICompiler"/> is not registered.</exception>
        /// <remarks>
        /// Use this method to access the compiler integration where constructor injection is not feasible.
        /// </remarks>
        public static ICompiler GetCompiler()
        {
            return Program.ServiceProvider.GetRequiredService<ICompiler>();
        }

        /// <summary>
        /// Gets the <see cref="IEmulatorLauncher"/> service from the application's service provider.
        /// </summary>
        /// <returns>The registered <see cref="IEmulatorLauncher"/> implementation.</returns>
        /// <exception cref="InvalidOperationException">Thrown if <see cref="IEmulatorLauncher"/> is not registered.</exception>
        /// <remarks>
        /// Use this to launch or control emulators from UI code when constructor injection is not available.
        /// </remarks>
        public static IEmulatorLauncher GetEmulatorLauncher()
        {
            return Program.ServiceProvider.GetRequiredService<IEmulatorLauncher>();
        }

        /// <summary>
        /// Gets a typed logger from the application's service provider.
        /// </summary>
        /// <typeparam name="T">The type for which the logger will be created (category).</typeparam>
        /// <returns>An <see cref="ILogger{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown if an <see cref="ILogger{T}"/> is not registered.</exception>
        /// <remarks>
        /// Example:
        /// <code language="csharp">
        /// var logger = ServiceHelper.GetLogger&lt;MyForm&gt;();
        /// logger.LogInformation("Started");
        /// </code>
        /// </remarks>
        public static ILogger<T> GetLogger<T>()
        {
            return Program.ServiceProvider.GetRequiredService<ILogger<T>>();
        }

        /// <summary>
        /// Gets a service of the specified type from the application's service provider.
        /// </summary>
        /// <typeparam name="T">The service type to resolve.</typeparam>
        /// <returns>The resolved service instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the requested service type is not registered.</exception>
        /// <remarks>
        /// This is a general-purpose helper. Prefer using specific accessors (for example <see cref="GetCompiler"/>)
        /// or constructor injection when possible.
        /// </remarks>
        public static T GetService<T>()
        {
            return Program.ServiceProvider.GetRequiredService<T>();
        }
    }
}
