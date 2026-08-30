# 03-tier1-wrapper: Upgrade cc65Wrapper to .NET 10

Upgrade the cc65Wrapper class library from net481 to net10.0. Update target framework, upgrade NuGet packages (Newtonsoft.Json 13.0.3 → 13.0.4), resolve any API compatibility issues from the 5 behavioral changes identified in assessment.

Assessment shows this project has minimal API surface impact — only 5 behavioral change issues detected. Package upgrades are straightforward with no incompatible dependencies.

After upgrade, validate that cc65WinForms (still on net481) still builds and references cc65Wrapper correctly via binding redirects or runtime resolution. This between-tier validation is critical to Bottom-Up strategy — ensures leaf libraries are stable before dependent apps upgrade.

**Done when**: cc65Wrapper targets net10.0, builds without errors/warnings, tests pass, cc65WinForms (on net481) still builds successfully
