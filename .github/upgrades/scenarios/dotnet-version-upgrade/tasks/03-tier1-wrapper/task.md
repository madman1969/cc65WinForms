# 03-tier1-wrapper: Upgrade cc65Wrapper to .NET 10

Upgrade the cc65Wrapper class library from net481 to net10.0. Update target framework, upgrade NuGet packages (Newtonsoft.Json 13.0.3 → 13.0.4), resolve any API compatibility issues from the 5 behavioral changes identified in assessment.

Assessment shows this project has minimal API surface impact — only 5 behavioral change issues detected. Package upgrades are straightforward with no incompatible dependencies.

After upgrade, validate that cc65WinForms (still on net481) still builds and references cc65Wrapper correctly via binding redirects or runtime resolution. This between-tier validation is critical to Bottom-Up strategy — ensures leaf libraries are stable before dependent apps upgrade.

**Done when**: cc65Wrapper targets net10.0, builds without errors/warnings, tests pass, cc65WinForms (on net481) still builds successfully

## Research Findings

### Project Dependencies (Verified via get_project_dependencies)
- **Package Management**: Standard (non-CPM) — packages defined directly in csproj with versions
- **NuGet Packages**:
  - CliWrap 2.4.0 (defined in cc65Wrapper.csproj) — no upgrade needed
  - Newtonsoft.Json 13.0.3 (defined in cc65Wrapper.csproj) — upgrade to 13.0.4 recommended
- **Framework References**:
  - Microsoft.CSharp
  - System.Net.Http
  - System.Data.DataSetExtensions

### API Compatibility Issues (5 behavioral changes)
All 5 issues are in `cc65Wrapper\Cc65CompilerConfiguration.cs` lines 76-80:
- `Environment.SetEnvironmentVariable(string, string)` behavioral change
- Used to set: CC65_HOME, CC65_INC, LD65_CFG, LD65_LIB, MAKE_HOME
- **Action**: Verify behavior after upgrade; no code changes expected (API still exists, behavior may differ slightly)

### Binding Redirect Issues (obsolete after upgrade)
Assessment flagged missing binding redirects in App.config for CliWrap and Newtonsoft.Json. These are irrelevant once targeting .NET 10 — modern .NET doesn't use App.config binding redirects.

### Affected Files
- `cc65Wrapper\cc65Wrapper.csproj` — TFM change + package version update
- `cc65Wrapper\App.config` — can be removed (obsolete in SDK-style .NET 10 projects)
- `cc65Wrapper\Cc65CompilerConfiguration.cs` — verify SetEnvironmentVariable behavior post-upgrade

## Execution Plan

1. **Update target framework** from net481 to net10.0 in cc65Wrapper.csproj
2. **Update Newtonsoft.Json** from 13.0.3 to 13.0.4
3. **Remove App.config** (no longer used in .NET 10 class libraries)
4. **Remove obsolete framework references** (if MSBuild errors occur) — Microsoft.CSharp, System.Net.Http, System.Data.DataSetExtensions should be automatic in .NET 10
5. **Build cc65Wrapper** and fix any errors
6. **Between-tier validation**: Build cc65WinForms (still on net481) to ensure it can reference the upgraded wrapper
7. **Verify SetEnvironmentVariable behavior** remains functional
