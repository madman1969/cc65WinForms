# 🎯 Quick Reference: Logging in cc65WinForms

## ✅ Logging is Already Enabled!

Just run your app in **Debug mode (F5)** and check:
**View → Output** (or `Ctrl+Alt+O`) → Select **Debug** in dropdown

---

## 🚀 Quick Start Examples

### Example 1: Use the Injected Compiler Service

`MainForm` receives `ICompiler` via constructor injection (see `Program.cs` /
`MainForm.cs`) rather than resolving it from a locator:

```csharp
private async void btnCompile_Click(object sender, EventArgs e)
{
	var result = await compiler.CompileAsync(project); // 'compiler' is the injected field

	// Check Debug Output window (or logs/app.log) to see logs
}
```

### Example 2: Existing Code Still Works

```csharp
// Your old code - now logs automatically!
var result = await Cc65Build.CompileAsync(project);
```

### Example 3: Add Logging to Your Form

Add `ILogger<T>` as another constructor parameter — it's resolved
automatically by the DI container once `services.AddLogging(...)` has run:

```csharp
using Microsoft.Extensions.Logging;
using cc65Wrapper.Abstractions;

public partial class MainForm : Form
{
	private readonly ILogger<MainForm> _logger;

	public MainForm(ICompiler compiler, IEmulatorLauncher emulatorLauncher, ILogger<MainForm> logger)
	{
		this.compiler = compiler;
		this.emulatorLauncher = emulatorLauncher;
		_logger = logger;
		InitializeComponent();
	}

	private void SomeMethod()
	{
		_logger.LogInformation("Something happened");
		_logger.LogWarning("Something unusual happened");
		_logger.LogError("Something bad happened");
	}
}
```

---

## 📺 What You'll See in Debug Output

```
[Debug] Starting compilation for project 'MyGame' targeting platform 'c64'
[Debug] Compiler command: cl65 -t c64 main.c -O -o output.prg
[Debug] Build phase changed: Compiling - Compiling...
[Debug] Command completed with exit code 0
[Information] Compilation completed successfully in 1234ms
```

---

## 🎛️ Change Log Verbosity

Edit the `#if DEBUG` / `#else` block inside `services.AddLogging(...)` in `Program.cs`:

```csharp
// More logs
builder.SetMinimumLevel(LogLevel.Debug);

// Fewer logs
builder.SetMinimumLevel(LogLevel.Information);

// Only errors
builder.SetMinimumLevel(LogLevel.Error);
```

---

## 📁 File Logging (Already Configured)

No setup needed — `Program.cs` already writes to `logs/app.log` (resolved via
`AppContext.BaseDirectory`, so the path doesn't depend on the working
directory the app was launched from):

```csharp
services.AddLogging(builder =>
{
	builder.AddDebug();
	builder.AddFile(logFilePath); // logs/app.log next to the executable
	builder.SetMinimumLevel(LogLevel.Debug);
});
```

---

## 🔍 Common Services

`MainForm` receives these via constructor injection rather than a locator —
add the parameter and the container supplies it:

```csharp
using cc65Wrapper.Abstractions;
using Microsoft.Extensions.Logging;

public MainForm(ICompiler compiler, IEmulatorLauncher emulatorLauncher, ILogger<MainForm> logger)
{
	// ...
}
```

---

## 💡 Pro Tips

✅ **Debug Output** shows logs immediately  
✅ **Log levels** work: Debug < Information < Warning < Error  
✅ **Old code** logs automatically (backward compatible)  
✅ **Progress events** are also logged  
✅ **All errors** include file name and line number  

---

## 🐛 Troubleshooting

**Not seeing logs?**
1. ✅ Debug mode? (F5, not Ctrl+F5)
2. ✅ Output window set to "Debug"?
3. ✅ MinimumLevel not too high?

**Too many logs?**
- Change `SetMinimumLevel(LogLevel.Information)`

**Want timestamps?**
- Debug output includes them automatically

---

## 📖 Full Documentation

- See `HOW_TO_USE_LOGGING.md` for detailed examples
- See `LOGGING_INFRASTRUCTURE.md` for architecture details

---

**That's it! Run your app and check the Debug Output window! 🚀**
