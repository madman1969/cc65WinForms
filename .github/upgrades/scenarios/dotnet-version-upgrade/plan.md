# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade cc65WinForms solution from .NET Framework 4.8.1 to .NET 10  
**Scope**: 2 projects (~4k LOC), Windows Forms desktop application with class library

### Selected Strategy
**Bottom-Up (Dependency-First)** — Upgrade from leaf nodes to root applications, tier by tier.  
**Rationale**: 2 projects with 2-tier dependency graph; Framework→Core boundary requires different upgrade mechanics per layer.

### Dependency Graph
```
Tier 2: cc65WinForms (WinForms app)
		 ↓
Tier 1: cc65Wrapper (Class library)
```

### Per-Tier Summary

**Tier 1 — cc65Wrapper**
- Single class library with minimal dependencies
- Wrapper for cc65 compiler command-line interface
- Dependencies: 2 NuGet packages (CliWrap, Newtonsoft.Json)
- Completion criteria: Builds successfully on net10.0, tests pass, cc65WinForms (still on net481) still builds

**Tier 2 — cc65WinForms**
- Windows Forms desktop application
- Dependencies: cc65Wrapper (upgraded in Tier 1), FCTB package
- Requires Windows Desktop support (net10.0-windows)
- Completion criteria: Builds successfully, all functionality preserved, full test suite passes

## Tasks

### 01-prerequisites: Verify tooling and environment

Ensure .NET 10 SDK is installed and properly configured. Validate that global.json (if present) doesn't pin to an incompatible SDK version. Check that Visual Studio is configured to discover .NET 10 SDK installations.

This task identifies environment blockers before starting project changes, preventing wasted effort on unmigrateable code.

**Done when**: .NET 10 SDK detected via `dotnet --list-sdks`, no global.json conflicts, Visual Studio tooling functional

---

### 02-sdk-conversion: Convert projects to SDK-style format

Convert cc65WinForms.csproj and cc65Wrapper.csproj from legacy .NET Framework project format to SDK-style format while staying on net481. This modernizes the project structure without changing the target framework yet.

Assessment shows both projects use old-style csproj format (verbose XML with explicit file listings, packages.config for NuGet). SDK-style format is a prerequisite for targeting modern .NET and enables simpler project files with wildcard includes and inline package references.

This task must complete before TFM changes — never merge SDK conversion with framework upgrades. The conversion rewrites project structure significantly; framework changes touch references and code. Keeping them separate makes issues easier to diagnose and roll back if needed.

**Done when**: Both projects converted to SDK-style format, still targeting net481, solution builds without errors or warnings, all tests pass

---

### 03-tier1-wrapper: Upgrade cc65Wrapper to .NET 10

Upgrade the cc65Wrapper class library from net481 to net10.0. Update target framework, upgrade NuGet packages (Newtonsoft.Json 13.0.3 → 13.0.4), resolve any API compatibility issues from the 5 behavioral changes identified in assessment.

Assessment shows this project has minimal API surface impact — only 5 behavioral change issues detected. Package upgrades are straightforward with no incompatible dependencies.

After upgrade, validate that cc65WinForms (still on net481) still builds and references cc65Wrapper correctly via binding redirects or runtime resolution. This between-tier validation is critical to Bottom-Up strategy — ensures leaf libraries are stable before dependent apps upgrade.

**Done when**: cc65Wrapper targets net10.0, builds without errors/warnings, tests pass, cc65WinForms (on net481) still builds successfully

---

### 04-tier2-winforms: Upgrade cc65WinForms to .NET 10

Upgrade the cc65WinForms application from net481 to net10.0-windows with Windows Desktop support enabled. Update project properties to include `<TargetFramework>net10.0-windows</TargetFramework>` and `<UseWindowsForms>true</UseWindowsForms>`.

Assessment identified 2,831 Windows Forms API compatibility issues (91.5% binary incompatible) — primarily type references that require recompilation, not code changes. The majority are ToolStripMenuItem, ToolStripButton, Label, TextBox, and other standard WinForms controls that work on modern .NET with proper project configuration.

Key migration challenges:
- **Legacy WinForms controls** (164 issues): StatusBar, ContextMenu, MainMenu, MenuItem, ToolBar have been removed — replace with ToolStrip, MenuStrip, ContextMenuStrip equivalents
- **System.Drawing APIs** (235 issues): Available via Windows Desktop support or System.Drawing.Common NuGet package for server scenarios
- **Configuration system** (2 issues): app.config migration if custom sections exist

Upgrade FCTB package if needed. Fix breaking API changes identified in assessment. Verify all forms load correctly, UI controls render properly, and application functionality is preserved.

**Done when**: cc65WinForms targets net10.0-windows with Windows Desktop enabled, builds without errors/warnings, application runs successfully with all UI functional, full test coverage passes

---

### 05-enable-cpm: Enable Central Package Management

Add Directory.Packages.props to the solution root to centralize NuGet package version management across both projects. Extract package versions from project files into the central location.

Currently each project manages its own package versions independently. With only 2 projects this is manageable, but CPM provides version consistency, easier maintenance, and prevents accidental version mismatches as the solution grows.

**Done when**: Directory.Packages.props created with all package versions centralized, both projects reference packages without versions, solution builds and restores correctly, no duplicate version declarations

---

### 06-final-validation: Validate complete upgrade

Perform comprehensive validation of the upgraded solution. Build entire solution from clean state, run full test suite, verify runtime behavior of cc65WinForms application with various inputs, validate performance characteristics haven't degraded.

Document any findings, recommendations for future improvements (nullable reference types adoption, modern C# feature opportunities, code modernization suggestions), and any deferred issues that don't block the upgrade.

Create summary of changes made, confirm no regressions in functionality, verify the solution is deployment-ready on .NET 10.

**Done when**: Clean build succeeds, all tests pass, application verified functional, upgrade summary documented, no blocking issues remain
