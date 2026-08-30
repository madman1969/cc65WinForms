# 02-sdk-conversion: Convert projects to SDK-style format

Convert cc65WinForms.csproj and cc65Wrapper.csproj from legacy .NET Framework project format to SDK-style format while staying on net481. This modernizes the project structure without changing the target framework yet.

Assessment shows both projects use old-style csproj format (verbose XML with explicit file listings, packages.config for NuGet). SDK-style format is a prerequisite for targeting modern .NET and enables simpler project files with wildcard includes and inline package references.

This task must complete before TFM changes — never merge SDK conversion with framework upgrades. The conversion rewrites project structure significantly; framework changes touch references and code. Keeping them separate makes issues easier to diagnose and roll back if needed.

**Done when**: Both projects converted to SDK-style format, still targeting net481, solution builds without errors or warnings, all tests pass
