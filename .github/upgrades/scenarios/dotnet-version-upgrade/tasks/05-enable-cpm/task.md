# 05-enable-cpm: Enable Central Package Management

Add Directory.Packages.props to the solution root to centralize NuGet package version management across both projects. Extract package versions from project files into the central location.

Currently each project manages its own package versions independently. With only 2 projects this is manageable, but CPM provides version consistency, easier maintenance, and prevents accidental version mismatches as the solution grows.

**Done when**: Directory.Packages.props created with all package versions centralized, both projects reference packages without versions, solution builds and restores correctly, no duplicate version declarations

## Research Findings

### Current Package State (from previous tasks)

**cc65Wrapper.csproj packages:**
- CliWrap 3.6.6
- Newtonsoft.Json 13.0.4

**cc65WinForms.csproj packages:**
- FCTB 2.16.24

**No version conflicts detected** — each package is used by only one project.

### Scope
- Solution root: `C:\GitHub\cc65WinForms`
- Both projects in solution: cc65Wrapper, cc65WinForms
- Directory.Packages.props location: solution root

### Execution Plan (following converting-to-cpm skill)

1. **Establish baseline**: Verify solution builds (already confirmed ✅)
2. **Create Directory.Packages.props**: Use `dotnet new packagesprops` or manual creation
3. **Add PackageVersion entries** for all 3 packages
4. **Update cc65Wrapper.csproj**: Remove Version from PackageReference
5. **Update cc65WinForms.csproj**: Remove Version from PackageReference
6. **Restore and validate**: Ensure solution builds correctly
7. **Verify no version changes**: Compare package resolution before/after

### Expected Directory.Packages.props Content

```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>
  <ItemGroup>
    <PackageVersion Include="CliWrap" Version="3.6.6" />
    <PackageVersion Include="FCTB" Version="2.16.24" />
    <PackageVersion Include="Newtonsoft.Json" Version="13.0.4" />
  </ItemGroup>
</Project>
```
