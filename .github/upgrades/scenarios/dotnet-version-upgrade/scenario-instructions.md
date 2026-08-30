# .NET Version Upgrade to .NET 10

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0 (.NET 10 LTS)

## Source Control
- **Source Branch**: master
- **Working Branch**: dotnet-version-upgrade-net10.0
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## Upgrade Options

### Strategy
- **Upgrade Strategy**: Bottom-Up

### Project Structure
- **Project Approach**: In-place
- **Package Management**: Enable CPM

### Compatibility
- **Unsupported API Handling**: Fix during upgrade
- **Windows Native APIs**: Enable Windows Desktop

## Strategy

**Selected**: Bottom-Up (Dependency-First)  
**Rationale**: 2-tier dependency graph with .NET Framework projects requiring different upgrade mechanics per layer. Wrapper library has minimal API impact; WinForms app has 2,831 compatibility issues requiring tier-by-tier validation.

### Execution Constraints
- Strict tier ordering: Tier 1 (cc65Wrapper) must complete and validate before Tier 2 (cc65WinForms)
- Between-tier validation: after upgrading Tier 1, confirm Tier 2 (still on net481) still builds
- SDK-style conversion separate from TFM upgrade — never merge these operations
- Per-tier flow: update all projects in tier → build → test → validate higher tiers → proceed
- Each tier validated independently with full test execution before next tier starts
