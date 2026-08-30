# Upgrade Options — cc65WinForms

Assessment: 2 projects (.NET Framework 4.8.1), WinForms desktop app with class library, 2,836+ API issues

## Strategy

### Upgrade Strategy
This solution has 2 .NET Framework projects requiring upgrade to modern .NET, which requires tier-by-tier validation with different upgrade mechanics per layer.

| Value | Description |
|-------|-------------|
| **Bottom-Up** (selected) | Upgrade leaf-node libraries first, then work upward through the dependency graph tier by tier |

## Project Structure

### Project Approach
Both projects are .NET Framework class libraries or WinForms apps without System.Web dependencies.

| Value | Description |
|-------|-------------|
| **In-place** (selected) | Replace target framework directly - clean approach when all projects migrate together |
| Multi-targeting | Add new TFM alongside existing (net481;net10.0-windows) to support gradual migration |

### Package Management
Solution has 2 projects without centralized package management, making version consistency harder to maintain.

| Value | Description |
|-------|-------------|
| **Enable CPM** (selected) | Add Directory.Packages.props to centralize package versions across all projects |
| Keep current | Leave packages.config or PackageReference as-is in each project file |

## Compatibility

### Unsupported API Handling
Assessment found 2,831 binary/source incompatible APIs requiring code changes.

| Value | Description |
|-------|-------------|
| **Fix during upgrade** (selected) | Address breaking API changes as part of each project's upgrade task |
| Document and defer | Create tracking issues for breaking changes, handle after TFM upgrade |

### Windows Native APIs
WinForms project uses System.Drawing and Windows-specific APIs that need special configuration.

| Value | Description |
|-------|-------------|
| **Enable Windows Desktop** (selected) | Target net10.0-windows and add UseWindowsForms/UseWPF properties to support Windows Desktop APIs |
| System.Drawing.Common NuGet | Add System.Drawing.Common package and handle cross-platform limitations |
