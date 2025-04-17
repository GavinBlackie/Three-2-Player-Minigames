using CheckConnectDot_MAUI_App.ConnectFour;
// Author: Artem Kotliar
// This is the ConnectFourPage class, used to interact with the ConnectFourGame.xaml

namespace CheckConnectDot_MAUI_App;

public partial class ConnectFourPage : ContentPage
{
    private ConnectFourGame _connectFourGame;
    private Image[,] _boardImages;

	public ConnectFourPage(ConnectFourGame connectFourGame)
	{
		_connectFourGame = connectFourGame; // Contain the given singleton of a connectFourGame
    
        InitializeComponent();
        InitializeBoardReferences();
        SetParams();
        UpdateTurnDisplay();
    }

    private void InitializeBoardReferences()
    {
        _boardImages = new Image[7, 6];
        
        // Column 0
        _boardImages[0, 0] = Cell_0_0;
        _boardImages[0, 1] = Cell_0_1;
        _boardImages[0, 2] = Cell_0_2;
        _boardImages[0, 3] = Cell_0_3;
        _boardImages[0, 4] = Cell_0_4;
        _boardImages[0, 5] = Cell_0_5;
        
        // Column 1
        _boardImages[1, 0] = Cell_1_0;
        _boardImages[1, 1] = Cell_1_1;
        _boardImages[1, 2] = Cell_1_2;
        _boardImages[1, 3] = Cell_1_3;
        _boardImages[1, 4] = Cell_1_4;
        _boardImages[1, 5] = Cell_1_5;
        
        // Column 2
        _boardImages[2, 0] = Cell_2_0;
        _boardImages[2, 1] = Cell_2_1;
        _boardImages[2, 2] = Cell_2_2;
        _boardImages[2, 3] = Cell_2_3;
        _boardImages[2, 4] = Cell_2_4;
        _boardImages[2, 5] = Cell_2_5;
        
        // Column 3
        _boardImages[3, 0] = Cell_3_0;
        _boardImages[3, 1] = Cell_3_1;
        _boardImages[3, 2] = Cell_3_2;
        _boardImages[3, 3] = Cell_3_3;
        _boardImages[3, 4] = Cell_3_4;
        _boardImages[3, 5] = Cell_3_5;
        
        // Column 4
        _boardImages[4, 0] = Cell_4_0;
        _boardImages[4, 1] = Cell_4_1;
        _boardImages[4, 2] = Cell_4_2;
        _boardImages[4, 3] = Cell_4_3;
        _boardImages[4, 4] = Cell_4_4;
        _boardImages[4, 5] = Cell_4_5;
        
        // Column 5
        _boardImages[5, 0] = Cell_5_0;
        _boardImages[5, 1] = Cell_5_1;
        _boardImages[5, 2] = Cell_5_2;
        _boardImages[5, 3] = Cell_5_3;
        _boardImages[5, 4] = Cell_5_4;
        _boardImages[5, 5] = Cell_5_5;
        
        // Column 6
        _boardImages[6, 0] = Cell_6_0;
        _boardImages[6, 1] = Cell_6_1;
        _boardImages[6, 2] = Cell_6_2;
        _boardImages[6, 3] = Cell_6_3;
        _boardImages[6, 4] = Cell_6_4;
        _boardImages[6, 5] = Cell_6_5;
    }

    /// <summary>
    /// Handles a column button click event in the Connect Four game
    /// Drops a disk in the selected column, updates the UI, and checks for a win
    /// </summary>
    protected void OnCol(object sender, EventArgs e)
    {
        if (_connectFourGame.GameOver)
        {
            return;
        }
        if (sender is Button col && col.CommandParameter is int columnIndex)
        {
            bool moveSuccessful = _connectFourGame.DropDisk(columnIndex, UpdateCellImage, UpdateDiskCounts);

            if (!moveSuccessful)
            {
                DisplayAlert("Invalid Move", "Column is full!", "OK");
                return;
            }

            if (_connectFourGame.GameOver && _connectFourGame.Winner != null)
            {
                DisplayAlert("Game Over", $"Player {_connectFourGame.Winner.Number} wins!", "OK");
                if (_connectFourGame.Winner.Number == 1)
                {
                    int wins = _connectFourGame.Players[0].NumWins += 1;
                    _lbl1Wins.Text = $"Player {_connectFourGame.Players[0].Number} Wins: {wins.ToString()}";
                }
                else
                {
                    int wins = _connectFourGame.Players[1].NumWins += 1;
                    _lbl2Wins.Text = $"Player {_connectFourGame.Players[1].Number} Wins: {wins.ToString()}";
                }
                // Reset game and board UI
                _connectFourGame.ResetGame();
                ResetBoardUI();
            }
            else
            {
                UpdateTurnDisplay();
            }
        }
    }
    
    /// <summary>
    /// Resets the visual game board by clearing all disk images and resetting the disk counters for both players
    /// </summary>
    private void ResetBoardUI()
    {
        for (int col = 0; col < 7; col++)
        {
            for (int row = 0; row < 6; row++)
            {
                _boardImages[col, row].Source = "empty_space.png";
            }
        }
        // Reset disk counters
        UpdateDiskCounts(21, 21);
    }
    
    /// <summary>
    /// Updates the image displayed in a specific cell on the game board setting it to the player's disk image.
    /// </summary>
    /// <param name="col">The column index</param>
    /// <param name="row">The row index</param>
    /// <param name="imageSource">The image file name to display</param>
    private void UpdateCellImage(int col, int row, string imageSource)
    {
        if (col >= 0 && col < 7 && row >= 0 && row < 6)
        {
            _boardImages[col, row].Source = imageSource;
        }
    }

    /// <summary>
    /// Updates the turn label to show which player's turn it is and changes the label color based on the player.
    /// </summary>
    private void UpdateTurnDisplay()
    {
        TurnLabel.Text = $"Player {_connectFourGame.CurrentPlayer.Number}'s Turn!";
        TurnLabel.TextColor = _connectFourGame.CurrentPlayer.Number == 1 ? Colors.Red : Colors.Blue;
    }

    /// <summary>
    /// Updates the screen display showing the remaining number of disks for each player
    /// Also changes the text color to red if the count is low (5 or fewer)
    /// </summary>
    /// <param name="diskCount1"></param>
    /// <param name="diskCount2"></param>
    private void UpdateDiskCounts(int diskCount1, int diskCount2)
    {
        diskAMT1.Text = diskCount1.ToString();
        diskAMT2.Text = diskCount2.ToString();
        
        // Change text color when running low
        diskAMT1.TextColor = diskCount1 <= 5 ? Colors.Red : Colors.White;
        diskAMT2.TextColor = diskCount2 <= 5 ? Colors.Red : Colors.White;
    }

    /// <summary>
    /// Sets the command parameters and event handlers for each column button
    /// </summary>
    protected void SetParams()
    {
        Col1.CommandParameter = 1;
        Col1.Clicked += OnCol;
        
        Col2.CommandParameter = 2;
        Col2.Clicked += OnCol;
        
        Col3.CommandParameter = 3;
        Col3.Clicked += OnCol;
        
        Col4.CommandParameter = 4;
        Col4.Clicked += OnCol;
        
        Col5.CommandParameter = 5;
        Col5.Clicked += OnCol;
        
        Col6.CommandParameter = 6;
        Col6.Clicked += OnCol;
        
        Col7.CommandParameter = 7;
        Col7.Clicked += OnCol;
    }

    /// <summary>
    /// Handles the forfeit button click event
    /// </summary>
    private async void OnForfeit(object sender, EventArgs e)
    {
        if (_connectFourGame.GameOver)
        {
            return;
        }

        // Ask for confirmation
        bool confirm = await DisplayAlert(
            "Forfeit Game", 
            $"Player {_connectFourGame.CurrentPlayer.Number}, are you sure you want to forfeit?", 
            "Yes", "No");

        if (confirm)
        {
            // The forfeiting player loses, the other player wins
            Player winner = _connectFourGame.CurrentPlayer.Number == 1 ? _connectFourGame.Players[1] : _connectFourGame.Players[0];

            // Update win
            winner.NumWins++;

            // Update UI
            if (winner.Number == 1)
            {
                _lbl1Wins.Text = $"Player {winner.Number} Wins: {winner.NumWins}";
            }
            else
            {
                _lbl2Wins.Text = $"Player {winner.Number} Wins: {winner.NumWins}";
            }

            // Show result
            await DisplayAlert(
                "Game Forfeited", 
                $"Player {_connectFourGame.CurrentPlayer.Number} forfeited!\nPlayer {winner.Number} wins by default.", 
                "OK");

            // Reset the game
            _connectFourGame.ResetGame();
            ResetBoardUI();
        }
    }
    /// <summary>
    /// Handles the save button click event to store player data
    /// </summary>
    private void OnSave(object sender, EventArgs e)
    {
        try
        {
            JSONHandler.SaveGameData(_connectFourGame.PlayerTuple);
            DisplayAlert("Success", "Game data saved!", "OK");
        }
        catch (Connect4Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }
    
    /// <summary>
    /// Loads saved player data and updates the UI accordingly
    /// </summary>
    private void LoadPlayerData()
    {
        try
        {
            PlayerData playerData = JSONHandler.LoadGameData();
        
            // Update Player 1
            _connectFourGame.PlayerTuple.Item1.Name = playerData.Player1Name;
            _connectFourGame.PlayerTuple.Item1.NumWins = playerData.Player1Wins;
        
            // Update Player 2
            _connectFourGame.PlayerTuple.Item2.Name = playerData.Player2Name;
            _connectFourGame.PlayerTuple.Item2.NumWins = playerData.Player2Wins;
        
            // Update UI
            _lbl1Wins.Text = $"{playerData.Player1Name} Wins: {playerData.Player1Wins}";
            _lbl2Wins.Text = $"{playerData.Player2Name} Wins: {playerData.Player2Wins}";
        }
        catch (Connect4Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }
    
    /// <summary>
    /// Called automatically when the page becomes visible to the user
    /// Loads player data from storage when the game screen appears
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPlayerData();
    }
}