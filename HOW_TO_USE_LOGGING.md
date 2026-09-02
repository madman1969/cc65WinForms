# How to Use Logging in cc65WinForms

## ✅ Logging is Now Enabled!

The logging infrastructure has been configured to output to the **Visual Studio Debug Output** window.

## 📺 Viewing Logs

### In Visual Studio:
1. Run your application in Debug mode (F5)
2. Go to **View** > **Output** (or press `Ctrl+Alt+O`)
3. In the "Show output from:" dropdown, select **Debug**
4. You'll see logs like this:

```
[Debug] Starting compilation for project 'MyGame' targeting platform 'c64'
[Debug] Compiler command: cl65 -t c64 main.c -O -o build/mygame.prg
[Information] Compilation completed successfully in 1243ms
```

## 🎯 Log Levels Configured

- **Debug Mode**: Shows DEBUG, INFO, WARNING, ERROR logs
- **Release Mode**: Shows only INFO, WARNING, ERROR logs

## 💻 Using Logging in Your Code

### Option 1: Use the Service-Based API via Constructor Injection (Recommended)

`MainForm` takes `ICompiler` and `IEmulatorLauncher` as constructor parameters,
resolved by the DI container in `Program.cs` (`services.AddSingleton<MainForm>()` /
`serviceProvider.GetRequiredService<MainForm>()`) instead of `new MainForm()`:

```csharp
using cc65Wrapper;
using cc65Wrapper.Abstractions;

public partial class MainForm : Form
{
	private readonly ICompiler compiler;
	private readonly IEmulatorLauncher emulatorLauncher;

	public MainForm(ICompiler compiler, IEmulatorLauncher emulatorLauncher)
	{
		this.compiler = compiler;
		this.emulatorLauncher = emulatorLauncher;
		InitializeComponent();
	}

	private async void btnCompile_Click(object sender, EventArgs e)
	{
		var project = new CC65Project
		{
			ProjectName = "MyGame",
			WorkingDirectory = @"C:\Projects\MyGame",
			InputFiles = new[] { "main.c" },
			TargetPlatform = CC65ProjectTypes.c64
		};

		// Compile - all steps will be logged automatically
		var result = await compiler.CompileAsync(project);

		if (result.Success)
		{
			MessageBox.Show("Compilation succeeded!");
		}
		else
		{
			MessageBox.Show($"Compilation failed with {result.Errors.Count} errors");
		}
	}
}
```

### Option 2: Continue Using Legacy Static API (Also Works)

Your existing code using `Cc65Build.CompileAsync()` will also log automatically:

```csharp
// Your existing code - now with logging!
var result = await Cc65Build.CompileAsync(project);

// Logs will appear in the Debug Output window:
// [Debug] Starting compilation...
// [Debug] Compiler command: cl65 ...
// [Information] Compilation completed successfully in 1500ms
```

### Option 3: Add Custom Logging to Your Forms

Add an `ILogger<T>` constructor parameter alongside your other injected
dependencies — `Microsoft.Extensions.Logging` registers `ILogger<T>`
automatically once `services.AddLogging(...)` has been called, so no extra
registration is needed:

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

	private void btnCompile_Click(object sender, EventArgs e)
	{
		_logger.LogInformation("User clicked Compile button");

		try
		{
			// Your compilation code
			_logger.LogInformation("Compilation started");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Compilation failed with exception");
			MessageBox.Show($"Error: {ex.Message}");
		}
	}
}
```

## 🔧 File Logging (Already Configured)

`Program.cs` already logs to a file — no setup needed:

```csharp
services.AddLogging(builder =>
{
	builder.AddDebug();               // Visual Studio Output
	builder.AddFile(logFilePath);     // File logging

#if DEBUG
	builder.SetMinimumLevel(LogLevel.Debug);
#else
	builder.SetMinimumLevel(LogLevel.Information);
#endif
});
```

Logs are written to `logs/app.log`, resolved via `AppContext.BaseDirectory` so
the path is always relative to the executable rather than whatever directory
the process happens to be launched from (this matters because Visual Studio,
VS Code, and a desktop shortcut can each default to a different working
directory).

## 📊 What Gets Logged

### Compilation Operations:
- ✅ Project validation
- ✅ Command-line arguments
- ✅ Compilation progress (0%, 10%, 30%, 70%, 100%)
- ✅ Each compiler error/warning with file and line number
- ✅ Compilation duration
- ✅ Success/failure status

### Emulator Operations:
- ✅ Emulator path selection
- ✅ Launch arguments
- ✅ Launch success/failure
- ✅ Exit codes

### Error Parsing:
- ✅ Number of errors found
- ✅ Number of warnings found
- ✅ Which parser handled each error line

## 🎨 Custom Log Viewer (Optional)

You can add a log viewer to your WinForms app:

```csharp
// Add a TextBox named txtLogViewer to your form

using Microsoft.Extensions.Logging;

public class FormLoggerProvider : ILoggerProvider
{
	private readonly TextBox _textBox;

	public FormLoggerProvider(TextBox textBox)
	{
		_textBox = textBox;
	}

	public ILogger CreateLogger(string categoryName)
	{
		return new FormLogger(_textBox, categoryName);
	}

	public void Dispose() { }
}

public class FormLogger : ILogger
{
	private readonly TextBox _textBox;
	private readonly string _categoryName;

	public FormLogger(TextBox textBox, string categoryName)
	{
		_textBox = textBox;
		_categoryName = categoryName;
	}

	public IDisposable BeginScope<TState>(TState state) => null;

	public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
		Exception exception, Func<TState, Exception, string> formatter)
	{
		if (!IsEnabled(logLevel))
			return;

		var message = $"[{DateTime.Now:HH:mm:ss}] [{logLevel}] {formatter(state, exception)}";

		// Thread-safe UI update
		if (_textBox.InvokeRequired)
		{
			_textBox.Invoke(new Action(() => 
			{
				_textBox.AppendText(message + Environment.NewLine);
			}));
		}
		else
		{
			_textBox.AppendText(message + Environment.NewLine);
		}
	}
}

// In Program.cs, add:
services.AddLogging(builder =>
{
	builder.AddDebug();
	// builder.AddProvider(new FormLoggerProvider(yourTextBox)); // Add after form creation
});
```

## 🚀 Quick Test

Run your application and trigger a compilation. You should see output in the Debug window like:

```
[12:34:56 DBG] Starting compilation for project 'MyProject' targeting platform 'c64'
[12:34:56 DBG] Build phase changed: Initializing - Initializing...
[12:34:56 DBG] Build phase changed: ValidatingProject - Validating project...
[12:34:56 DBG] Build phase changed: BuildingArguments - Building arguments...
[12:34:56 DBG] Compiler command: cl65 -t c64 main.c -O -o build/output.prg
[12:34:56 DBG] Build phase changed: Compiling - Compiling...
[12:34:56 DBG] Executing command: cl65 -t c64 main.c -O -o build/output.prg
[12:34:57 DBG] Command completed with exit code 0
[12:34:57 DBG] Build phase changed: ParsingErrors - Parsing errors...
[12:34:57 DBG] Parsing 0 error lines using 4 parser(s)
[12:34:57 DBG] Error parsing completed: found 0 error(s), 0 warning(s)
[12:34:57 DBG] Build phase changed: Completed - Completed
[12:34:57 INF] Compilation completed successfully in 1234ms
```

## 🔍 Troubleshooting

### Not Seeing Logs?

1. **Check the Output window is on "Debug"**, not "Build" or other options
2. **Make sure you're running in Debug mode** (F5), not Release
3. **Check the log level** - Debug logs won't show if minimum level is Information

### Too Many Logs?

Change minimum level in Program.cs:

```csharp
builder.SetMinimumLevel(LogLevel.Information); // or Warning, Error
```

### Want Console Window?

Add this to Program.cs before `Application.Run()`:

```csharp
#if DEBUG
AllocConsole(); // Shows a console window
services.AddLogging(builder => builder.AddConsole());
#endif

[System.Runtime.InteropServices.DllImport("kernel32.dll")]
private static extern bool AllocConsole();
```

---

## 📝 Summary

✅ **Logging is enabled and configured**  
✅ **Outputs to both the Visual Studio Debug window and `logs/app.log`**  
✅ **Works with both new and legacy APIs**  
✅ **No code changes required to existing functionality**  
✅ **Logs compilation, emulator launch, and errors automatically**  
✅ **Unhandled exceptions on the UI thread or elsewhere are logged instead of lost** (see `Program.cs`)  

**Start your app in Debug mode and check View > Output > Debug to see logs!** 🎉
