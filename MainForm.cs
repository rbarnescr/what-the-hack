using System;
using System.Windows.Forms;

namespace WhackAMole;

public partial class MainForm : Form
{
    private const int GridSize = 4; // 4x4 grid
    private const int GameDuration = 30; // 30 seconds
    private const int MoleAppearanceInterval = 1500; // milliseconds

    private Button[,] gridButtons = null!;
    private int score = 0;
    private int timeRemaining = GameDuration;
    private bool gameRunning = false;
    private Random random = new();

    private System.Windows.Forms.Timer? gameTimer;
    private System.Windows.Forms.Timer? moleTimer;

    public MainForm()
    {
        InitializeComponent();
        SetupGame();
    }

    private void InitializeComponent()
    {
        this.Text = "Whack-a-Mole Game";
        this.Size = new System.Drawing.Size(600, 750);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = System.Drawing.Color.LightGreen;

        // Title Label
        Label titleLabel = new()
        {
            Text = "WHACK-A-MOLE",
            Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.DarkGreen,
            Dock = DockStyle.Top,
            Height = 50,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        };
        this.Controls.Add(titleLabel);

        // Info Panel
        Panel infoPanel = new()
        {
            Dock = DockStyle.Top,
            Height = 80,
            BackColor = System.Drawing.Color.White
        };
        this.Controls.Add(infoPanel);

        // Score Label
        Label scoreLabel = new()
        {
            Name = "scoreLabel",
            Text = "Score: 0",
            Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.Blue,
            AutoSize = false,
            Bounds = new System.Drawing.Rectangle(20, 10, 250, 30),
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        };
        infoPanel.Controls.Add(scoreLabel);

        // Timer Label
        Label timerLabel = new()
        {
            Name = "timerLabel",
            Text = "Time: 30s",
            Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.Red,
            AutoSize = false,
            Bounds = new System.Drawing.Rectangle(300, 10, 250, 30),
            TextAlign = System.Drawing.ContentAlignment.MiddleRight
        };
        infoPanel.Controls.Add(timerLabel);

        // Game Status Label
        Label statusLabel = new()
        {
            Name = "statusLabel",
            Text = "Ready to start! Click 'Start Game' button.",
            Font = new System.Drawing.Font("Arial", 10),
            ForeColor = System.Drawing.Color.DarkGreen,
            AutoSize = false,
            Bounds = new System.Drawing.Rectangle(20, 45, 500, 30),
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        };
        infoPanel.Controls.Add(statusLabel);

        // Game Board Panel
        Panel boardPanel = new()
        {
            Dock = DockStyle.Top,
            Height = 400,
            Padding = new Padding(20),
            BackColor = System.Drawing.Color.LightGreen
        };
        this.Controls.Add(boardPanel);

        // Create grid buttons
        gridButtons = new Button[GridSize, GridSize];
        int buttonSize = (boardPanel.Width - 40 - (GridSize - 1) * 10) / GridSize;

        for (int row = 0; row < GridSize; row++)
        {
            for (int col = 0; col < GridSize; col++)
            {
                int r = row;  // Capture loop variable value
                int c = col;  // Capture loop variable value
                
                Button btn = new()
                {
                    Width = buttonSize,
                    Height = buttonSize,
                    Left = 20 + col * (buttonSize + 10),
                    Top = 20 + row * (buttonSize + 10),
                    BackColor = System.Drawing.Color.Gray,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = false // false = no mole, true = mole present
                };

                btn.Click += (s, e) => Button_Click(r, c);
                btn.FlatAppearance.BorderSize = 2;
                btn.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;

                boardPanel.Controls.Add(btn);
                gridButtons[row, col] = btn;
            }
        }

        // Button Panel
        Panel buttonPanel = new()
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            BackColor = System.Drawing.Color.White,
            Padding = new Padding(10)
        };
        this.Controls.Add(buttonPanel);

        // Start Button
        Button startButton = new()
        {
            Name = "startButton",
            Text = "START GAME",
            Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
            Width = 120,
            Height = 40,
            Left = 20,
            Top = 10,
            BackColor = System.Drawing.Color.Green,
            ForeColor = System.Drawing.Color.White,
            Cursor = Cursors.Hand
        };
        startButton.Click += StartButton_Click;
        buttonPanel.Controls.Add(startButton);

        // Reset Button
        Button resetButton = new()
        {
            Name = "resetButton",
            Text = "RESET",
            Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
            Width = 120,
            Height = 40,
            Left = 150,
            Top = 10,
            BackColor = System.Drawing.Color.Orange,
            ForeColor = System.Drawing.Color.White,
            Cursor = Cursors.Hand
        };
        resetButton.Click += ResetButton_Click;
        buttonPanel.Controls.Add(resetButton);

        // Exit Button
        Button exitButton = new()
        {
            Name = "exitButton",
            Text = "EXIT",
            Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
            Width = 120,
            Height = 40,
            Left = 280,
            Top = 10,
            BackColor = System.Drawing.Color.Red,
            ForeColor = System.Drawing.Color.White,
            Cursor = Cursors.Hand
        };
        exitButton.Click += (s, e) => this.Close();
        buttonPanel.Controls.Add(exitButton);
    }

    private void SetupGame()
    {
        score = 0;
        timeRemaining = GameDuration;
        gameRunning = false;
        UpdateUI();

        // Initialize timers
        gameTimer = new()
        {
            Interval = 1000 // 1 second
        };
        gameTimer.Tick += GameTimer_Tick;

        moleTimer = new()
        {
            Interval = MoleAppearanceInterval
        };
        moleTimer.Tick += MoleTimer_Tick;
    }

    private void StartButton_Click(object? sender, EventArgs e)
    {
        if (!gameRunning)
        {
            gameRunning = true;
            score = 0;
            timeRemaining = GameDuration;

            // Reset all buttons
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    gridButtons[row, col].Tag = false;
                    gridButtons[row, col].BackColor = System.Drawing.Color.Gray;
                    gridButtons[row, col].Text = "";
                }
            }

            UpdateUI();

            // Start timers
            gameTimer?.Start();
            moleTimer?.Start();
        }
    }

    private void ResetButton_Click(object? sender, EventArgs e)
    {
        gameRunning = false;
        gameTimer?.Stop();
        moleTimer?.Stop();

        score = 0;
        timeRemaining = GameDuration;

        // Reset all buttons
        for (int row = 0; row < GridSize; row++)
        {
            for (int col = 0; col < GridSize; col++)
            {
                gridButtons[row, col].Tag = false;
                gridButtons[row, col].BackColor = System.Drawing.Color.Gray;
                gridButtons[row, col].Text = "";
            }
        }

        UpdateUI();
    }

    private void GameTimer_Tick(object? sender, EventArgs e)
    {
        timeRemaining--;
        UpdateUI();

        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }

    private void MoleTimer_Tick(object? sender, EventArgs e)
    {
        if (gameRunning)
        {
            // Hide all moles
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    gridButtons[row, col].Tag = false;
                    gridButtons[row, col].BackColor = System.Drawing.Color.Gray;
                    gridButtons[row, col].Text = "";
                }
            }

            // Show random mole
            int randomRow = random.Next(GridSize);
            int randomCol = random.Next(GridSize);
            gridButtons[randomRow, randomCol].Tag = true;
            gridButtons[randomRow, randomCol].BackColor = System.Drawing.Color.Gold;
            gridButtons[randomRow, randomCol].Text = "🏆";
            gridButtons[randomRow, randomCol].Font = new System.Drawing.Font("Arial", 20);
        }
    }

    private void Button_Click(int row, int col)
    {
        if (gameRunning && gridButtons[row, col].Tag is true)
        {
            score++;
            gridButtons[row, col].Tag = false;
            gridButtons[row, col].BackColor = System.Drawing.Color.LightGreen;
            gridButtons[row, col].Text = "✓";
            gridButtons[row, col].Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
            gridButtons[row, col].ForeColor = System.Drawing.Color.Green;

            UpdateUI();

            // Reset button appearance after hit
            System.Windows.Forms.Timer hitTimer = new()
            {
                Interval = 200
            };
            hitTimer.Tick += (s, e) =>
            {
                gridButtons[row, col].BackColor = System.Drawing.Color.Gray;
                gridButtons[row, col].Text = "";
                gridButtons[row, col].ForeColor = System.Drawing.Color.Black;
                hitTimer.Stop();
                hitTimer.Dispose();
            };
            hitTimer.Start();
        }
    }

    private void EndGame()
    {
        gameRunning = false;
        gameTimer?.Stop();
        moleTimer?.Stop();

        // Hide all moles
        for (int row = 0; row < GridSize; row++)
        {
            for (int col = 0; col < GridSize; col++)
            {
                gridButtons[row, col].Tag = false;
                gridButtons[row, col].BackColor = System.Drawing.Color.Gray;
                gridButtons[row, col].Text = "";
            }
        }

        UpdateUI();

        MessageBox.Show(
            $"Game Over!\n\nFinal Score: {score}",
            "Game Results",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }

    private void UpdateUI()
    {
        // Update score label
        if (this.Controls.Find("scoreLabel", true).FirstOrDefault() is Label scoreLabel)
        {
            scoreLabel.Text = $"Score: {score}";
        }

        // Update timer label
        if (this.Controls.Find("timerLabel", true).FirstOrDefault() is Label timerLabel)
        {
            timerLabel.Text = $"Time: {timeRemaining}s";
        }

        // Update status label
        if (this.Controls.Find("statusLabel", true).FirstOrDefault() is Label statusLabel)
        {
            if (gameRunning)
            {
                statusLabel.Text = "Game in progress! Whack the moles!";
            }
            else if (timeRemaining == GameDuration && score == 0)
            {
                statusLabel.Text = "Ready to start! Click 'START GAME' button.";
            }
            else
            {
                statusLabel.Text = "Game ended! Click 'RESET' to play again.";
            }
        }
    }
}
