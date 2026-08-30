# 06-final-validation: Validate complete upgrade

Perform comprehensive validation of the upgraded solution. Build entire solution from clean state, run full test suite, verify runtime behavior of cc65WinForms application with various inputs, validate performance characteristics haven't degraded.

Document any findings, recommendations for future improvements (nullable reference types adoption, modern C# feature opportunities, code modernization suggestions), and any deferred issues that don't block the upgrade.

Create summary of changes made, confirm no regressions in functionality, verify the solution is deployment-ready on .NET 10.

**Done when**: Clean build succeeds, all tests pass, application verified functional, upgrade summary documented, no blocking issues remain

## Research Findings

### Pre-Validation State (from previous tasks)
- ✅ Both projects upgraded: cc65Wrapper → net10.0, cc65WinForms → net10.0-windows
- ✅ SDK-style conversion completed for both projects
- ✅ CliWrap migrated from 2.4.0 → 3.6.6 with API updates
- ✅ Newtonsoft.Json upgraded to 13.0.4
- ✅ FCTB 2.16.24 kept (compatible)
- ✅ CPM enabled with Directory.Packages.props
- ✅ TabStrip.dll local reference retained
- ✅ Range ambiguity resolved with FastColoredTextBoxNS qualification
- ✅ Designer serialization warnings fixed (WFO1000)
- ✅ All obsolete framework references removed

### Build Tool Selection (per building-projects skill)
**Use dotnet build** — correct choice for this solution:
- Both projects are SDK-style ✅
- Target modern .NET only (no net4xx TFMs) ✅
- WinForms project has Windows Desktop support enabled ✅
- No WPF/XAML/COM/T4/vcxproj present ✅
- No complex .resx with embedded binaries reported ✅

### Test Discovery
**No test projects** detected in the solution (2 projects total: 1 library, 1 WinForms app). Final validation will focus on:
1. Clean full-solution build
2. Application launch and UI functionality
3. Manual smoke testing of core features

### Validation Checklist

**Build Validation:**
- [ ] Clean full-solution build with `dotnet build`
- [ ] Verify no errors
- [ ] Verify no warnings
- [ ] Confirm all projects build for correct TFMs

**Runtime Validation:**
- [ ] Application launches without errors
- [ ] Main form renders correctly
- [ ] Project load/save functionality works
- [ ] CC65 build/compiler integration verified
- [ ] Emulator launch tested
- [ ] Editor functionality (syntax highlighting, save, etc.)

**CPM Validation:**
- [ ] Confirm package resolution from Directory.Packages.props
- [ ] No version conflicts or incorrect versions

**Documentation:**
- [ ] Upgrade summary with before/after state
- [ ] Recommendations for future improvements
- [ ] Known issues or deferred work
