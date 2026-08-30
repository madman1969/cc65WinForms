# 05-enable-cpm: Enable Central Package Management

Add Directory.Packages.props to the solution root to centralize NuGet package version management across both projects. Extract package versions from project files into the central location.

Currently each project manages its own package versions independently. With only 2 projects this is manageable, but CPM provides version consistency, easier maintenance, and prevents accidental version mismatches as the solution grows.

**Done when**: Directory.Packages.props created with all package versions centralized, both projects reference packages without versions, solution builds and restores correctly, no duplicate version declarations
