# 04-tier2-winforms: Upgrade cc65WinForms to .NET 10

Upgrade the cc65WinForms application from net481 to net10.0-windows with Windows Desktop support enabled. Update project properties to include `<TargetFramework>net10.0-windows</TargetFramework>` and `<UseWindowsForms>true</UseWindowsForms>`.

Assessment identified 2,831 Windows Forms API compatibility issues (91.5% binary incompatible) — primarily type references that require recompilation, not code changes. The majority are ToolStripMenuItem, ToolStripButton, Label, TextBox, and other standard WinForms controls that work on modern .NET with proper project configuration.

Key migration challenges:
- **Legacy WinForms controls** (164 issues): StatusBar, ContextMenu, MainMenu, MenuItem, ToolBar have been removed — replace with ToolStrip, MenuStrip, ContextMenuStrip equivalents
- **System.Drawing APIs** (235 issues): Available via Windows Desktop support or System.Drawing.Common NuGet package for server scenarios
- **Configuration system** (2 issues): app.config migration if custom sections exist

Upgrade FCTB package if needed. Fix breaking API changes identified in assessment. Verify all forms load correctly, UI controls render properly, and application functionality is preserved.

**Done when**: cc65WinForms targets net10.0-windows with Windows Desktop enabled, builds without errors/warnings, application runs successfully with all UI functional, full test coverage passes
