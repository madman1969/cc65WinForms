---
uid: LoggingInfrastructure
title: Logging Infrastructure for cc65Wrapper
---
## Overview

The cc65Wrapper now includes comprehensive logging infrastructure using **Microsoft.Extensions.Logging**, providing:

- ✅ **Structured logging** with semantic log messages
- ✅ **High-performance logging** using source generators (LoggerMessage pattern)
- ✅ **Automatic fallback** to NullLogger when logging is not configured
- ✅ **Full integration** with all major services (Compiler, Emulator, Parser, CommandExecutor)
- ✅ **Event-based logging** with unique event IDs for easy filtering
- ✅ **Multiple log levels** (Debug, Information, Warning, Error)

## Quick Start

### 1. Basic Usage (Without Logging)

The library works **without any logging configuration** using automatic NullLogger fallback:

```csharp
var services = new ServiceCollection();
services.AddCc65Wrapper();
var serviceProvider = services.BuildServiceProvider();

var compiler = serviceProvider.GetRequiredService<ICompiler>();
var result = await compiler.CompileAsync(project);
```

### 2. With Console Logging

```csharp
var services = new ServiceCollection();

// Add logging
services.AddLogging(builder =>
{
	builder.AddConsole();
	builder.SetMinimumLevel(LogLevel.Information);
});

// Add cc65Wrapper services
services.AddCc65Wrapper();

var serviceProvider = services.BuildServiceProvider();
var compiler = serviceProvider.GetRequiredService<ICompiler>();
```

### 3. With Multiple Providers

```csharp
services.AddLogging(builder =>
{
	builder.AddConsole();
	builder.AddDebug();
	builder.AddFile("logs/cc65wrapper.log"); // Requires Serilog or NLog
	builder.SetMinimumLevel(LogLevel.Debug);
});
```

### 4. Compact Registration

```csharp
services.AddCc65Wrapper(builder =>
{
	builder.AddConsole();
	builder.SetMinimumLevel(LogLevel.Information);
});
```

### 5. Legacy Static API (Backward Compatible)

For existing code using static methods, configure logging globally:

```csharp
using cc65Wrapper.Logging;
using Microsoft.Extensions.Logging;

// Create logger factory
var loggerFactory = LoggerFactory.Create(builder =>
{
	builder.AddConsole();
	builder.SetMinimumLevel(LogLevel.Information);
});

// Set global factory for static API usage
Cc65LoggerFactory.SetLoggerFactory(loggerFactory);

// Now legacy code automatically logs
var result = await Cc65Build.CompileAsync(project);
```

## Log Categories

The library uses the following logger categories:

| Category | Description |
|----------|-------------|
| `cc65Wrapper.Services.Cc65Compiler` | Compilation operations |
| `cc65Wrapper.Services.Cc65EmulatorLauncher` | Emulator launching |
| `cc65Wrapper.Infrastructure.CliWrapCommandExecutor` | Command execution |
| `cc65Wrapper.Parsers.ErrorParser` | Error parsing |

## Event IDs

All log messages include unique event IDs for easy filtering:

| Range | Category | Examples |
|-------|----------|----------|
| 1000-1999 | Compilation | 1000: Started, 1001: Succeeded, 1002: Failed |
| 2000-2999 | Emulator | 2000: Started, 2001: Launched, 2002: Failed |
| 3000-3999 | Command Execution | 3000: Executing, 3001: Completed, 3002: Failed |
| 4000-4999 | Error Parsing | 4000: Started, 4001: Line Parsed, 4003: Completed |
| 5000-5999 | Validation | 5000: Failed, 5001: Warning |
| 6000-6999 | Configuration | 6000: Loaded, 6001: Missing, 6002: Saved |
| 7000-7999 | File System | 7000: Directory Changed, 7002: File Not Found |

## Filtering Examples

### Filter by Event ID (Console)

```csharp
services.AddLogging(builder =>
{
	builder.AddConsole(options =>
	{
		options.LogToStandardErrorThreshold = LogLevel.Warning;
	});
	builder.AddFilter("cc65Wrapper.Services.Cc65Compiler", LogLevel.Debug);
	builder.AddFilter("cc65Wrapper.Services.Cc65EmulatorLauncher", LogLevel.Information);
});
```

### Filter by Event ID Range (Serilog)

```csharp
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.Filter.ByIncludingOnly(evt => 
		evt.Properties.TryGetValue("EventId", out var eventId) &&
		eventId.ToString().StartsWith("1")) // Only compilation events
	.WriteTo.Console()
	.WriteTo.File("logs/compilation.log")
	.CreateLogger();
```

## Sample Log Output

### Compilation

```
[12:34:56 INF] Starting compilation for project 'MyGame' targeting platform 'c64' (EventId: 1000)
[12:34:56 DBG] Compiler command: cl65 -t c64 main.c -O -o build/mygame.prg (EventId: 1003)
[12:34:56 DBG] Build phase changed: Compiling - Compiling... (EventId: 1006)
[12:34:56 DBG] Executing command: cl65 -t c64 main.c -O -o build/mygame.prg (EventId: 3000)
[12:34:57 DBG] Command completed with exit code 0 (EventId: 3001)
[12:34:57 DBG] Parsing 0 error lines using 4 parser(s) (EventId: 4000)
[12:34:57 INF] Compilation completed successfully in 1243ms (EventId: 1001)
```

### Compilation with Errors

```
[12:35:10 INF] Starting compilation for project 'MyGame' targeting platform 'c64' (EventId: 1000)
[12:35:10 DBG] Compiler command: cl65 -t c64 main.c -O -o build/mygame.prg (EventId: 1003)
[12:35:11 DBG] Parsing 3 error lines using 4 parser(s) (EventId: 4000)
[12:35:11 DBG] Parser 'FourPartErrorParser' handled error line: main.c:42:Error:Syntax error (EventId: 4001)
[12:35:11 ERR] Compilation error at main.c:42 - Syntax error (EventId: 1005)
[12:35:11 DBG] Error parsing completed: found 2 error(s), 1 warning(s) (EventId: 4003)
[12:35:11 ERR] Compilation failed with 2 error(s) in 987ms (EventId: 1002)
```

### Emulator Launch

```
[12:36:00 INF] Starting emulator 'C:\Emulators\VICE\x64sc.exe' for platform 'c64' (EventId: 2000)
[12:36:00 DBG] Emulator command: C:\Emulators\VICE\x64sc.exe -autostart build/mygame.prg (EventId: 2003)
[12:36:01 DBG] Command completed with exit code 0 (EventId: 3001)
[12:36:01 INF] Emulator launched successfully with PID 0 (EventId: 2001)
```

## Advanced Scenarios

### Custom Logger Implementation

```csharp
public class CustomCompilerLogger : ILogger<Cc65Compiler>
{
	public IDisposable BeginScope<TState>(TState state) => null;

	public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

	public void Log<TState>(
		LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception exception,
		Func<TState, Exception, string> formatter)
	{
		// Custom logging logic
		Console.WriteLine($"[{eventId.Id}] {formatter(state, exception)}");
	}
}

services.AddSingleton<ILogger<Cc65Compiler>, CustomCompilerLogger>();
```

### Progress Tracking with Logging

```csharp
var progress = new Progress<BuildProgressEventArgs>(e =>
{
	// Progress reports are also logged automatically
	Console.WriteLine($"{e.Phase}: {e.Message} ({e.PercentComplete}%)");
});

var result = await compiler.CompileAsync(project, progress);
```

### Structured Logging with Serilog

```csharp
services.AddLogging(builder =>
{
	var logger = new LoggerConfiguration()
		.MinimumLevel.Debug()
		.Enrich.FromLogContext()
		.WriteTo.Console(outputTemplate: 
			"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
			"(EventId: {EventId:0000}) {NewLine}{Exception}")
		.WriteTo.File(
			"logs/cc65wrapper.json",
			rollingInterval: RollingInterval.Day,
			formatter: new JsonFormatter())
		.CreateLogger();

	builder.AddSerilog(logger);
});
```

## Performance

The logging infrastructure uses:

- **Source generators** (LoggerMessage pattern) for zero-allocation logging
- **Lazy evaluation** - arguments only computed if log level is enabled
- **NullLogger fallback** - zero overhead when logging is not configured
- **Minimal string allocations** - structured logging with parameters

## Migration Guide

### From Old Code (No Logging)

```csharp
// Old code - still works!
var result = await Cc65Build.CompileAsync(project);

// Add logging if desired
LoggerFactory.SetLoggerFactory(myLoggerFactory);
```

### New Service-Based Code

```csharp
// Inject logger via DI
public class MyCompilerService
{
	private readonly ICompiler _compiler;

	public MyCompilerService(ICompiler compiler)
	{
		_compiler = compiler; // Logging already configured!
	}

	public async Task CompileAsync(CC65Project project)
	{
		// All logging happens automatically
		var result = await _compiler.CompileAsync(project);
	}
}
```

## Troubleshooting

### No Logs Appearing

1. Check minimum log level: `builder.SetMinimumLevel(LogLevel.Debug)`
2. Ensure logger provider is added: `builder.AddConsole()`
3. Verify category filter allows cc65Wrapper logs

### Too Many Logs

```csharp
// Reduce verbosity
builder.SetMinimumLevel(LogLevel.Warning);

// Or filter specific categories
builder.AddFilter("cc65Wrapper.Infrastructure", LogLevel.Warning);
builder.AddFilter("cc65Wrapper.Parsers", LogLevel.Error);
```

### Static API Not Logging

```csharp
// Must set global factory for static methods
Cc65LoggerFactory.SetLoggerFactory(myLoggerFactory);
```

## Configuration Examples

### appsettings.json (ASP.NET Core / Generic Host)

```json
{
  "Logging": {
	"LogLevel": {
	  "Default": "Information",
	  "cc65Wrapper": "Debug",
	  "cc65Wrapper.Services": "Information",
	  "cc65Wrapper.Infrastructure": "Warning"
	}
  }
}
```

### Programmatic Configuration

```csharp
services.AddLogging(builder =>
{
	builder.AddConfiguration(configuration.GetSection("Logging"));
	builder.AddConsole();
	builder.AddDebug();

	// Override for specific categories
	builder.AddFilter("cc65Wrapper.Services.Cc65Compiler", LogLevel.Trace);
});
```

---

## Summary

The logging infrastructure provides comprehensive observability into the cc65Wrapper operations while maintaining backward compatibility and zero overhead when logging is not configured. All logs use structured logging for easy querying and filtering in log aggregation systems like Seq, Splunk, or Azure Application Insights.

**Key Benefits:**
- ✅ Zero-configuration default (NullLogger)
- ✅ High-performance source-generated logging
- ✅ Full backward compatibility
- ✅ Rich structured logging with event IDs
- ✅ Easy integration with any logging provider
