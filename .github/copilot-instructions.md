# Copilot Instructions - Whack-a-Mole Game

## Project Context

This is the **Whack-a-Mole Game** - a Windows Forms-based game built in C# using .NET 8.0. The game tests player reflexes by having them click on moles that randomly appear on a 4x4 grid before they disappear.

### Technology Stack
- **Language**: C# (.NET 8.0)
- **UI Framework**: Windows Forms
- **Platform**: Windows
- **Project Type**: Desktop Application (WPF/Forms)

## Game Overview

### Core Mechanics
- **Game Board**: 4x4 grid of buttons representing mole positions
- **Gameplay Duration**: 30 seconds per game session
- **Mole Behavior**: Moles appear randomly at different positions every 500ms
- **Player Interaction**: Click on moles to "whack" them before they disappear
- **Scoring**: Each successful click increments the score
- **Visual Feedback**: Instant feedback when hitting moles

### Key Features
1. **Score Tracking** - Persistent score display during gameplay
2. **Game Timer** - 30-second countdown timer
3. **Random Generation** - Random mole positions for replayability
4. **UI Responsiveness** - Smooth button interactions and animations

## Project Structure

```
copilot-workshop/what-the-hack/
├── MainForm.cs           # Main game window and UI logic
├── Program.cs            # Application entry point
├── WhackAMole.csproj     # Project configuration
├── README.md             # User documentation
├── .github/
│   └── copilot-instructions.md  # This file
├── bin/                  # Compiled binaries
├── obj/                  # Build artifacts
└── copilot-workshop.sln  # Solution file
```

## Code Organization

### MainForm.cs
- Contains the main game window (Form)
- Game board UI with button grid
- Event handlers for button clicks
- Timer management for game duration
- Score tracking and display logic
- Mole appearance/disappearance logic

### Program.cs
- Application entry point
- Initializes and runs MainForm

## Enhancement: Game Record History Feature

**Issue #3**: Add Game Record History Feature
**Status**: Enhancement (label applied)
**Description**: Save and display a record history of the most successful games played.

### Proposed Implementation
- Save game records (score, timestamp, duration) to persistent storage
- Create a history view showing:
  - Top 10 highest scores
  - Game timestamps
  - Date range filtering
  - Option to clear history
- Add "View History" button in main menu
- Include confirmation dialog for history clearing

## When Working with This Project, Consider

### Development Guidelines
1. **Maintain Windows Forms conventions** - Use standard WinForms patterns
2. **Performance** - Keep mole appearance timing responsive (500ms intervals)
3. **User Experience** - Ensure clear visual feedback for game events
4. **Code Style** - Follow C# naming conventions (PascalCase for classes/methods)
5. **Error Handling** - Gracefully handle edge cases and invalid states

### Common Tasks

#### Adding New Game Features
- Modify MainForm.cs for UI changes
- Update game timer logic if changing duration
- Extend scoring system if adding point multipliers
- Add persistence layer for game history feature

#### Testing Gameplay
1. Build project: `dotnet build`
2. Run game: `dotnet run`
3. Test mole clicking responsiveness
4. Verify score increments correctly
5. Confirm 30-second timer works accurately

#### Building and Running
```bash
# Build
dotnet build

# Run
dotnet run

# Clean
dotnet clean
```

## Integration with GitHub

This project is integrated with GitHub MCP for Copilot Chat:
- **Repository**: rbarnescr/what-the-hack
- **Issues**: Query existing issues via MCP tools
- **Enhancement Label**: Applied to feature requests
- **Discussion**: Use GitHub issues for feature planning

### Using Copilot Chat for Game Development
```
@github List all issues in rbarnescr/what-the-hack
@github Get details about issue #3
@github Create an issue for a new game feature
```

## Copilot Suggestions

When helping with this project, Copilot should:

1. **Suggest Enhancements** in context of C# Windows Forms patterns
2. **Maintain Game Balance** - Consider difficulty progression
3. **Improve UX** - Suggest UI improvements for clarity
4. **Add Features** - Consider the game record history feature
5. **Code Quality** - Suggest refactoring for maintainability

### Example Copilot Interactions

**Query Game Mechanics**
```
"How can I make the mole appearance timing more consistent?"
"What's the best way to implement the game history feature?"
"How should I structure the score tracking?"
```

**Suggest Improvements**
```
"What accessibility improvements could help players?"
"How can I add difficulty levels to the game?"
"Should I add sound effects or visual animations?"
```

**GitHub Integration**
```
"Show me issue #3 details for the game history feature"
"Create an issue for adding difficulty levels"
"List all open enhancement issues"
```

## Development Tips

### Debugging Gameplay
- Use breakpoints in event handlers to trace button clicks
- Monitor timer behavior for accurate 30-second duration
- Log score changes to verify calculation logic
- Test with multiple rapid clicks to check responsiveness

### Performance Considerations
- 500ms mole appearance interval is reasonable for reflexes
- 4x4 grid (16 buttons) is lightweight for WinForms
- Game runs entirely on UI thread - keep event handlers lean

### Future Enhancements (Beyond Issue #3)
- Add difficulty levels (faster mole appearance)
- Implement sound effects for hits/misses
- Add combo multipliers for consecutive hits
- Create leaderboard system
- Add power-ups or special moles
- Implement game statistics and analytics

## Resources

### C# Windows Forms Documentation
- [Microsoft Docs: Windows Forms](https://docs.microsoft.com/dotnet/desktop/winforms/)
- [Button Click Events](https://docs.microsoft.com/dotnet/api/system.windows.forms.control.click)
- [Timer Component](https://docs.microsoft.com/dotnet/api/system.windows.forms.timer)

### Game Development Best Practices
- Keep game loop responsive (especially for reaction-based games)
- Provide clear visual feedback for all interactions
- Test edge cases (rapid clicking, edge of grid)
- Balance difficulty to keep gameplay engaging

### .NET Resources
- [.NET 8 Documentation](https://docs.microsoft.com/dotnet/core/whats-new/dotnet-8)
- [C# Programming Guide](https://docs.microsoft.com/dotnet/csharp/)
- [Build Windows Forms Apps](https://docs.microsoft.com/dotnet/desktop/winforms/getting-started/)

## GitHub Issues Reference

**Existing Issues**:
- Issue #3: Add Game Record History Feature (Enhancement)

**Development Status**:
- [x] Core game mechanics implemented
- [x] Score tracking working
- [x] 30-second timer functional
- [ ] Game history feature (Issue #3 - in progress)
- [ ] Persistence layer (needed for history)
- [ ] History UI components (future)

## Quick Commands

```bash
# Navigate to project
cd C:\Workspace\copilot-workshop\what-the-hack

# Build the project
dotnet build

# Run the game
dotnet run

# Clean build artifacts
dotnet clean

# Check project info
dotnet project-info
```

## Copilot Chat Commands

Query game-related information:
```
@github Show issue #3 about game history
@github List all issues with "enhancement" label
@github Get repository info for rbarnescr/what-the-hack
```

## Notes for Copilot

- This is a straightforward reflection-based game - keep it fun and engaging
- The game record history (Issue #3) is the next major feature
- Consider user experience improvements when suggesting new features
- Maintain performance - don't add complex features that slow gameplay
- Keep the codebase simple and maintainable for a workshop project
