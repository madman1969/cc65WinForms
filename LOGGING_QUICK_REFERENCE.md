# 🎯 Quick Reference: Logging in cc65WinForms

## ✅ Logging is Already Enabled!

Just run your app in **Debug mode (F5)** and check:
**View → Output** (or `Ctrl+Alt+O`) → Select **Debug** in dropdown

---

## 🚀 Quick Start Examples

### Example 1: Use New Service-Based Compiler

```csharp
using cc65WinForms.Services;

private async void btnCompile_Click(object sender, EventArgs e)
{
	var compiler = ServiceHelper.GetCompiler(); // Logging enabled!
	var result = await compiler.CompileAsync(project);

	// Check Debug Output window to see logs
}
```

### Example 2: Existing Code Still Works

```csharp
// Your old code - now logs automatically!
var result = await Cc65Build.CompileAsync(project);
```

### Example 3: Add Logging to Your Form

```csharp
using Microsoft.Extensions.Logging;
using cc65WinForms.Services;

public partial class MainForm : Form
{
	private readonly ILogger<MainForm> _logger;

	public MainForm()
	{
		InitializeComponent();
		_logger = ServiceHelper.GetLogger<MainForm>();
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

Edit `Program.cs`, line ~27:

```csharp
// More logs
builder.SetMinimumLevel(LogLevel.Debug);

// Fewer logs
builder.SetMinimumLevel(LogLevel.Information);

// Only errors
builder.SetMinimumLevel(LogLevel.Error);
```

---

## 📁 Add File Logging (Optional)

### 1. Add to `Directory.Packages.props`:
```xml
<PackageVersion Include="Serilog.Extensions.Logging.File" Version="3.0.0" />
```

### 2. Add to `cc65WinForms/cc65WinForms.csproj`:
```xml
<PackageReference Include="Serilog.Extensions.Logging.File" />
```

### 3. Update `Program.cs`:
```csharp
services.AddLogging(builder =>
{
	builder.AddDebug();
	builder.AddFile("logs/cc65-{Date}.txt"); // Logs to file!
	builder.SetMinimumLevel(LogLevel.Debug);
});
```

---

## 🔍 Common Services

```csharp
using cc65WinForms.Services;

// Compiler
var compiler = ServiceHelper.GetCompiler();

// Emulator Launcher
var emulator = ServiceHelper.GetEmulatorLauncher();

// Logger
var logger = ServiceHelper.GetLogger<YourClass>();

// Any service
var service = ServiceHelper.GetService<IYourService>();
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
