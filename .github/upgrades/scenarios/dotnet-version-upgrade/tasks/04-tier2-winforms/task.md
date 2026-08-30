# 04-tier2-winforms: Upgrade cc65WinForms to .NET 10

Upgrade the cc65WinForms application from net481 to net10.0-windows with Windows Desktop support enabled. Update project properties to include `<TargetFramework>net10.0-windows</TargetFramework>` and `<UseWindowsForms>true</UseWindowsForms>`.

Assessment identified 2,831 Windows Forms API compatibility issues (91.5% binary incompatible) — primarily type references that require recompilation, not code changes. The majority are ToolStripMenuItem, ToolStripButton, Label, TextBox, and other standard WinForms controls that work on modern .NET with proper project configuration.

Key migration challenges:
- **Legacy WinForms controls** (164 issues): StatusBar, ContextMenu, MainMenu, MenuItem, ToolBar have been removed — replace with ToolStrip, MenuStrip, ContextMenuStrip equivalents
- **System.Drawing APIs** (235 issues): Available via Windows Desktop support or System.Drawing.Common NuGet package for server scenarios
- **Configuration system** (2 issues): app.config migration if custom sections exist

Upgrade FCTB package if needed. Fix breaking API changes identified in assessment. Verify all forms load correctly, UI controls render properly, and application functionality is preserved.

**Done when**: cc65WinForms targets net10.0-windows with Windows Desktop enabled, builds without errors/warnings, application runs successfully with all UI functional, full test coverage passes

## Research Findings

### Current Project State (Verified via get_project_dependencies and file inspection)
- **Already SDK-style**: ✅ Project was converted in task 02
- **UseWindowsForms**: ✅ Already enabled in csproj
- **ImportWindowsDesktopTargets**: Present (can be removed — redundant in modern SDK)

### Dependencies
**NuGet Packages:**
- FCTB 2.16.24 — marked compatible in assessment, no upgrade needed

**Local DLL References:**
- TabStrip.dll (version 2.0.2523.29272) — custom control, must verify .NET 10 compatibility

**Obsolete Assembly References (to be removed):**
- `<Reference Include="CliWrap, Version=2.4.0.0, ..."/>` — obsolete, resolved via cc65Wrapper project reference now
- `<Reference Include="Microsoft.CSharp" />` — automatic in .NET 10
- `<Reference Include="System.Net.Http" />` — automatic in .NET 10  
- `<Reference Include="System.Data.DataSetExtensions" />` — automatic in .NET 10

**Project References:**
- cc65Wrapper — now on net10.0 ✅

### Assessment Summary
- **2,833 total issues** (mostly binary incompatibility requiring recompilation)
- **No legacy controls detected** in search (StatusBar, ContextMenu, MainMenu, MenuItem, ToolBar) — good news!
- **Windows Forms**: 2,594 issues — primarily standard controls (recompilation solves most)
- **GDI+ / System.Drawing**: 235 issues — Windows Desktop support provides these
- **Legacy Configuration**: 2 issues — app.config may need review

### Execution Plan

1. **Update target framework** from `net481` to `net10.0-windows`
2. **Remove obsolete properties** — `<ImportWindowsDesktopTargets>` is redundant
3. **Remove obsolete assembly references** — CliWrap, Microsoft.CSharp, System.Net.Http, System.Data.DataSetExtensions
4. **Keep TabStrip.dll reference** — verify compatibility after build
5. **Build and address any breaking changes**
6. **Test application launch** and UI functionality
7. **Verify all forms load** without errors

### Risk Areas
- **TabStrip.dll** — local DLL may not be compatible with .NET 10 if it has .NET Framework dependencies
- **Designer-generated code** — may have type references that need regeneration
- **App.config** — check for custom sections that need migration
