# 01-prerequisites: Verify tooling and environment

Ensure .NET 10 SDK is installed and properly configured. Validate that global.json (if present) doesn't pin to an incompatible SDK version. Check that Visual Studio is configured to discover .NET 10 SDK installations.

This task identifies environment blockers before starting project changes, preventing wasted effort on unmigrateable code.

**Done when**: .NET 10 SDK detected via `dotnet --list-sdks`, no global.json conflicts, Visual Studio tooling functional
