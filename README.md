# Whack-a-Mole Game

A fun Windows Forms-based Whack-a-Mole game built in C#. Test your reflexes as you try to click on moles before they disappear!

## Features

- **4x4 Game Board**: A grid where moles randomly appear
- **Score Tracking**: Keeps track of how many moles you've successfully hit
- **Game Timer**: 30-second game sessions
- **Random Mole Appearance**: Moles appear at random positions every 500ms
- **Visual Feedback**: Get instant visual feedback when you hit a mole
- **Easy Controls**: Simple click-to-whack interface

## Requirements

- .NET 8.0 or higher
- Windows operating system
- Visual Studio Code or Visual Studio

## Building the Project

### Using the Command Line

1. **Navigate to the project directory:**
   ```bash
   cd c:\Workspace\copilot-workshop
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the project:**
   ```bash
   dotnet build
   ```

### Using Visual Studio Code

1. Open the project in VS Code
2. Use the integrated terminal (Ctrl + `)
3. Run: `dotnet build`

## Running the Game

### From Command Line

```bash
dotnet run
```

### From Visual Studio Code

1. Press `Ctrl + Shift + D` to open the Debug view
2. Select ".NET 5+ Console" as the environment if prompted
3. Press `F5` to run the application

Or use the terminal:
```bash
dotnet run
```

## How to Play

1. **Start the Game**: Click the "START GAME" button to begin
2. **Watch the Board**: Moles will appear randomly on the grid (shown as a brown square with 🦡)
3. **Whack the Moles**: Click on a mole to hit it and earn points
4. **Watch the Timer**: You have 30 seconds to get as many hits as possible
5. **Game Over**: When time runs out, your final score will be displayed
6. **Play Again**: Click "RESET" to play another round or "EXIT" to quit

## Controls

- **START GAME**: Begin a new game session
- **RESET**: Stop the current game and reset the score
- **EXIT**: Close the application

## Game Mechanics

- Each mole that appears lasts approximately 500ms before being replaced by another random mole
- You earn 1 point for each successful mole hit
- Only hits on active moles (brown squares) count toward your score
- The game runs for exactly 30 seconds per session

## Score System

- Successfully hitting a mole: **+1 Point**
- Final score is displayed when time runs out

## Troubleshooting

- **Build Fails**: Ensure you have .NET 8.0 or higher installed. Check with: `dotnet --version`
- **Game Won't Start**: Make sure Windows Forms components are available on your system
- **Slow Performance**: Close other applications to ensure smooth gameplay

## Project Structure

```
WhackAMole.csproj    - Project configuration file
Program.cs           - Application entry point
MainForm.cs          - Main game form and logic
README.md            - This file
```

Enjoy the game and see how high you can score! 🎮
