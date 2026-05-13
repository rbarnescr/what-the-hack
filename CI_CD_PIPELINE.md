# GitHub Actions CI/CD Pipeline

## Overview
This project includes a comprehensive GitHub Actions CI/CD pipeline for automated building, testing, and artifact generation of the Whack-a-Mole game.

## Workflow Files

### 1. **build.yml** - Main Build Pipeline
Located: `.github/workflows/build.yml`

**Triggers**:
- ✅ Push to `main` or `develop` branches
- ✅ Pull requests targeting `main` or `develop`
- ✅ Changes to C# source files or project configuration

**Jobs**:

#### Build Job
- **Runs on**: `windows-latest`
- **Purpose**: Compile and publish the WhackAMole project
- **Steps**:
  1. Checkout code with full history
  2. Setup .NET 8.0 runtime
  3. Restore NuGet dependencies
  4. Build in Release configuration
  5. Publish release build
  6. Upload artifacts for 30 days
  7. Generate build summary

**Output Artifacts**:
- `WhackAMole-Release-Windows`
- Contains complete published application
- Retention: 30 days

#### Code Quality Job
- **Runs on**: `windows-latest`
- **Purpose**: Verify project structure and generate reports
- **Dependencies**: Requires `build` job to succeed
- **Steps**:
  1. List all C# source files
  2. Display project configuration
  3. Generate quality report

#### Notification Jobs
- **Success Notification**: Runs when all checks pass
- **Failure Notification**: Runs if any job fails
- Both generate summary reports in GitHub UI

## Configuration Details

### .NET Version
- **Runtime**: .NET 8.0
- **Framework**: net8.0-windows
- **Language**: C# 12

### Build Configuration
- **Release Mode**: Optimized for performance
- **Verbosity**: Normal (standard output)
- **Platform**: Windows (for Windows Forms)

### Trigger Paths
Workflow runs when changes detected in:
- `MainForm.cs` - UI/Game logic
- `Program.cs` - Application entry point
- `*.csproj` - Project configuration
- `.github/workflows/build.yml` - Pipeline itself

## Usage

### Automatic Triggers
The pipeline runs automatically on:
1. Push to main/develop branches
2. Pull request creation/updates
3. Any changes to source files or config

### Manual Trigger (Optional)
To manually trigger from GitHub UI:
1. Go to **Actions** tab in repository
2. Select **Build and Test** workflow
3. Click **Run workflow**
4. Choose branch and click **Run**

### Viewing Results
1. Navigate to **Actions** tab
2. Select the workflow run
3. View logs for each job
4. Download artifacts from summary page

## Build Artifacts

### Location
- **GitHub UI**: Actions → Workflow Run → Artifacts
- **Download**: Available as ZIP file
- **Contents**: Complete published application

### What's Included
```
publish/
├── WhackAMole.exe              # Windows application executable
├── WhackAMole.dll              # Main assembly
├── WhackAMole.deps.json        # Dependency manifest
├── WhackAMole.runtimeconfig.json # Runtime configuration
└── [Other supporting files]    # .NET runtime dependencies
```

### Using Artifacts
1. Download ZIP from GitHub UI
2. Extract to desired location
3. Run `WhackAMole.exe` to launch game

## Environment Variables
- `DOTNET_SKIP_FIRST_TIME_EXPERIENCE`: Optimized for CI/CD
- `DOTNET_CLI_TELEMETRY_OPTOUT`: Disabled for CI/CD

## Success Criteria

The pipeline succeeds when:
- ✅ Code compiles without errors
- ✅ Release build completes successfully
- ✅ Artifacts are generated
- ✅ All jobs complete successfully

## Failure Scenarios

Pipeline fails if:
- ❌ Compilation errors detected
- ❌ Missing dependencies
- ❌ Project configuration issues
- ❌ Build timeout (default: 360 minutes)

## Performance Metrics

Typical Pipeline Runtime:
- **Setup**: ~30 seconds
- **Restore**: ~20 seconds
- **Build**: ~15 seconds
- **Publish**: ~10 seconds
- **Total**: ~75 seconds

## Future Enhancements

Potential additions:
- [ ] Unit test execution
- [ ] Code coverage reporting
- [ ] Static code analysis (StyleCop)
- [ ] Automated versioning
- [ ] Release creation automation
- [ ] Artifact signing
- [ ] Performance benchmarking
- [ ] Integration tests

## Troubleshooting

### Build Fails with Restore Errors
- Check NuGet package availability
- Verify `.csproj` dependencies
- Ensure .NET 8.0 is available

### Missing Artifacts
- Verify publish step completed
- Check artifact retention settings
- Ensure sufficient storage quota

### Slow Build Times
- Check for missing dependencies cache
- Review build configuration
- Consider parallel job execution

## Best Practices

1. **Commit Messages**: Use conventional commits (feat:, fix:, refactor:)
2. **Branch Names**: Use `feature/`, `bugfix/`, `hotfix/` prefixes
3. **Pull Requests**: Always create PRs for code review
4. **Artifacts**: Download and test locally before release
5. **Monitoring**: Check action logs regularly

## Security Notes

- No sensitive data in pipeline
- All dependencies from official NuGet feeds
- Windows-latest runner uses Microsoft-maintained images
- Artifacts stored securely in GitHub
- 30-day artifact retention for space efficiency

## Related Files

- `.github/workflows/build.yml` - This workflow
- `.github/copilot-instructions.md` - Project context
- `WhackAMole.csproj` - Project configuration
- `Program.cs` - Application entry point
- `MainForm.cs` - Game implementation

## Support

For pipeline issues:
1. Check GitHub Actions logs
2. Review build summary reports
3. Verify local build: `dotnet build WhackAMole.csproj`
4. Check project dependencies: `dotnet list package`

## Monitoring Dashboard

**GitHub UI Path**: 
`Your Repository → Actions → Build and Test`

Shows:
- ✅ All workflow runs with status
- 📊 Success/failure history
- ⏱️ Average runtime
- 📦 Artifact availability

---

**Last Updated**: May 13, 2026  
**Status**: ✅ Active and Operational
