using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using cc65Wrapper.Abstractions;

namespace cc65WinForms.Services
{
    /// <summary>
    /// Helper class to access DI services from WinForms
    /// </summary>
    public static class ServiceHelper
    {
        /// <summary>
        /// Gets the compiler service with logging enabled
        /// </summary>
        public static ICompiler GetCompiler()
        {
            return Program.ServiceProvider.GetRequiredService<ICompiler>();
        }

        /// <summary>
        /// Gets the emulator launcher service with logging enabled
        /// </summary>
        public static IEmulatorLauncher GetEmulatorLauncher()
        {
            return Program.ServiceProvider.GetRequiredService<IEmulatorLauncher>();
        }

        /// <summary>
        /// Gets a logger for the specified type
        /// </summary>
        public static ILogger<T> GetLogger<T>()
        {
            return Program.ServiceProvider.GetRequiredService<ILogger<T>>();
        }

        /// <summary>
        /// Gets a service of the specified type
        /// </summary>
        public static T GetService<T>()
        {
            return Program.ServiceProvider.GetRequiredService<T>();
        }
    }
}
