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
        this.Size = new System.Drawing.Size(700, 850);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = System.Drawing.Color.White;
        this.Font = new System.Drawing.Font("Segoe UI", 10);

        // Title Label
        Label titleLabel = new()
        {
            Text = "🎮 WHACK-A-MOLE 🎮",
            Font = new System.Drawing.Font("Segoe UI", 28, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(30, 100, 180),
            Dock = DockStyle.Top,
            Height = 70,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            BackColor = System.Drawing.Color.FromArgb(230, 244, 255)
        };
        this.Controls.Add(titleLabel);

        // Info Panel
        Panel infoPanel = new()
        {
            Dock = DockStyle.Top,
            Height = 100,
            BackColor = System.Drawing.Color.FromArgb(245, 250, 255),
            Padding = new Padding(15)
        };
        this.Controls.Add(infoPanel);

        // Score Label
        Label scoreLabel = new()
        {
            Name = "scoreLabel",
            Text = "⭐ Score: 0",
            Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(30, 100, 180),
            AutoSize = false,
            Bounds = new System.Drawing.Rectangle(15, 15, 250, 35),
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        };
        infoPanel.Controls.Add(scoreLabel);

        // Timer Label
        Label timerLabel = new()
        {
            Name = "timerLabel",
            Text = "⏱️ Time: 30s",
            Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(200, 50, 50),
            AutoSize = false,
            Bounds = new System.Drawing.Rectangle(350, 15, 250, 35),
            TextAlign = System.Drawing.ContentAlignment.MiddleRight
        };
        infoPanel.Controls.Add(timerLabel);

        // Game Status Label
        Label statusLabel = new()
        {
            Name = "statusLabel",
            Text = "Ready to start! Click 'START GAME' button.",
            Font = new System.Drawing.Font("Segoe UI", 11),
            ForeColor = System.Drawing.Color.FromArgb(80, 120, 160),
            AutoSize = false,
            Bounds = new System.Drawing.Rectangle(15, 55, 650, 30),
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        };
        infoPanel.Controls.Add(statusLabel);

        // Game Board Panel
        Panel boardPanel = new()
        {
            Dock = DockStyle.Top,
            Height = 450,
            Padding = new Padding(25),
            BackColor = System.Drawing.Color.White
        };
        this.Controls.Add(boardPanel);

        // Create grid buttons
        gridButtons = new Button[GridSize, GridSize];
        int buttonSize = (boardPanel.Width - 50 - (GridSize - 1) * 15) / GridSize;

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
                    Left = 25 + col * (buttonSize + 15),
                    Top = 25 + row * (buttonSize + 15),
                    BackColor = System.Drawing.Color.FromArgb(200, 230, 255),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(30, 100, 180),
                    Tag = false // false = no mole, true = mole present
                };

                btn.Click += (s, e) => Button_Click(r, c);
                btn.FlatAppearance.BorderSize = 2;
                btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(100, 180, 255);
                btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(170, 220, 255);
                btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(100, 180, 255);

                boardPanel.Controls.Add(btn);
                gridButtons[row, col] = btn;
            }
        }

        // Button Panel
        Panel buttonPanel = new()
        {
            Dock = DockStyle.Bottom,
            Height = 80,
            BackColor = System.Drawing.Color.FromArgb(240, 248, 255),
            Padding = new Padding(15)
        };
        this.Controls.Add(buttonPanel);

        // Start Button
        Button startButton = new()
        {
            Name = "startButton",
            Text = "▶ START GAME",
            Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
            Width = 140,
            Height = 45,
            Left = 15,
            Top = 15,
            BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
            ForeColor = System.Drawing.Color.White,
            Cursor = Cursors.Hand,
            FlatStyle = FlatStyle.Flat
        };
        startButton.FlatAppearance.BorderSize = 0;
        startButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
        startButton.Click += StartButton_Click;
        buttonPanel.Controls.Add(startButton);

        // Reset Button
        Button resetButton = new()
        {
            Name = "resetButton",
            Text = "🔄 RESET",
            Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
            Width = 140,
            Height = 45,
            Left = 170,
            Top = 15,
            BackColor = System.Drawing.Color.FromArgb(155, 89, 182),
            ForeColor = System.Drawing.Color.White,
            Cursor = Cursors.Hand,
            FlatStyle = FlatStyle.Flat
        };
        resetButton.FlatAppearance.BorderSize = 0;
        resetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(142, 68, 173);
        resetButton.Click += ResetButton_Click;
        buttonPanel.Controls.Add(resetButton);

        // Exit Button
        Button exitButton = new()
        {
            Name = "exitButton",
            Text = "✕ EXIT",
            Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
            Width = 140,
            Height = 45,
            Left = 325,
            Top = 15,
            BackColor = System.Drawing.Color.FromArgb(192, 57, 43),
            ForeColor = System.Drawing.Color.White,
            Cursor = Cursors.Hand,
            FlatStyle = FlatStyle.Flat
        };
        exitButton.FlatAppearance.BorderSize = 0;
        exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(169, 50, 38);
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
                    gridButtons[row, col].BackColor = System.Drawing.Color.FromArgb(200, 230, 255);
                    gridButtons[row, col].Text = "";
                    gridButtons[row, col].ForeColor = System.Drawing.Color.FromArgb(30, 100, 180);
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
                gridButtons[row, col].BackColor = System.Drawing.Color.FromArgb(200, 230, 255);
                gridButtons[row, col].Text = "";
                gridButtons[row, col].ForeColor = System.Drawing.Color.FromArgb(30, 100, 180);
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
                    gridButtons[row, col].BackColor = System.Drawing.Color.FromArgb(200, 230, 255);
                    gridButtons[row, col].Text = "";
                    gridButtons[row, col].ForeColor = System.Drawing.Color.FromArgb(30, 100, 180);
                }
            }

            // Show random mole
            int randomRow = random.Next(GridSize);
            int randomCol = random.Next(GridSize);
            gridButtons[randomRow, randomCol].Tag = true;
            gridButtons[randomRow, randomCol].BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            gridButtons[randomRow, randomCol].Text = "🏆";
            gridButtons[randomRow, randomCol].Font = new System.Drawing.Font("Segoe UI", 24);
            gridButtons[randomRow, randomCol].ForeColor = System.Drawing.Color.White;
        }
    }

    private void Button_Click(int row, int col)
    {
        if (gameRunning && gridButtons[row, col].Tag is true)
        {
            score++;
            gridButtons[row, col].Tag = false;
            gridButtons[row, col].BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            gridButtons[row, col].Text = "✓";
            gridButtons[row, col].Font = new System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold);
            gridButtons[row, col].ForeColor = System.Drawing.Color.White;

            UpdateUI();

            // Reset button appearance after hit
            System.Windows.Forms.Timer hitTimer = new()
            {
                Interval = 300
            };
            hitTimer.Tick += (s, e) =>
            {
                gridButtons[row, col].BackColor = System.Drawing.Color.FromArgb(200, 230, 255);
                gridButtons[row, col].Text = "";
                gridButtons[row, col].ForeColor = System.Drawing.Color.FromArgb(30, 100, 180);
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
                gridButtons[row, col].BackColor = System.Drawing.Color.FromArgb(200, 230, 255);
                gridButtons[row, col].Text = "";
                gridButtons[row, col].ForeColor = System.Drawing.Color.FromArgb(30, 100, 180);
            }
        }

        UpdateUI();

        MessageBox.Show(
            $"🎉 Game Over! 🎉\n\nFinal Score: {score} 🌟",
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
