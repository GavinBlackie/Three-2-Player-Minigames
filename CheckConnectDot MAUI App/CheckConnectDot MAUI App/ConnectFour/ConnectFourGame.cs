namespace CheckConnectDot_MAUI_App.ConnectFour
// Author: Artem Kotliar
// This is a ConnectFourGame class which is derived from a base Game class. This class represents the connect 4 game.

{
    public class ConnectFourGame : Game
    {
        private Player _currentPlayer;
        private Player[] _players;
        private Disk[,]? _board;
        private int _disksLeft1;
        private int _disksLeft2;
        private const int ROWS = 6;
        private const int COLS = 7;

        /// <summary>
        /// Property to get and set GameOver
        /// </summary>
        public bool GameOver { get; private set; }
        /// <summary>
        /// Property to get and set Winner
        /// </summary>
        public Player? Winner { get; private set; }

        /// <summary>
        /// Constructor for the ConnectFourGame class
        /// </summary>
        /// <param name="playersTuple">The player tuple</param>
        public ConnectFourGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _players = new Player[] { playersTuple.Item1, playersTuple.Item2 };
            _currentPlayer = _players[0];
            _board = new Disk[COLS, ROWS];
            _disksLeft1 = 21;
            _disksLeft2 = 21;
        }

        /// <summary>
        /// Property to get the current player
        /// </summary>
        public Player CurrentPlayer { get { return _currentPlayer; } }
        
        /// <summary>
        /// Property to get the players
        /// </summary>
        public Player[] Players { get { return _players; } }

        /// <summary>
        /// Method to drop the disk in the game
        /// </summary>
        /// <param name="column">The column of the disk</param>
        /// <param name="updateCell">Callback to update the visual cell on the UI</param>
        /// <param name="updateDisks">Callback to update the remaining disk counts on the UI</param>
        /// <returns>True if the disk was successfully dropped and false if the column is full</returns>
        /// <exception cref="Connect4Exception">Thrown when a player has no disks left to drop</exception>
        public bool DropDisk(int column, Action<int, int, string> updateCell, Action<int, int> updateDisks)
        {
            // Check if current player has disks left
            if ((_currentPlayer.Number == 1 && _disksLeft1 <= 0) ||
                (_currentPlayer.Number == 2 && _disksLeft2 <= 0))
            {
                throw new Connect4Exception("Player has no disks left");
            }
            
            // Convert to 0-based index
            int col = column - 1;

            for (int row = ROWS - 1; row >= 0; row--)
            {
                if (_board[col, row] == null) // Check if empty
                {
                    // Create and place the disk
                    Disk disk = new PlayerDisk(col, row, _currentPlayer.Number);
                    _board[col, row] = disk;

                    if (_currentPlayer.Number == 1)
                    {
                        _disksLeft1--;
                    }
                    else
                    {
                        _disksLeft2--;
                    }
                    
                    // Update the UI
                    updateCell(col, row, disk.ImageSource);
                    updateDisks(_disksLeft1, _disksLeft2);
                    
                    // OnPlace method to play the sound
                    disk.OnPlace();
                    
                    if (CheckWin(col, row))
                    {
                        GameOver = true;
                        Winner = _currentPlayer;
                    }
                    
                    // Switch players
                    if (!GameOver)
                    {
                        _currentPlayer = _currentPlayer == _players[0] ? _players[1] : _players[0];
                    }
                    return true;
                }
            }
            // If we get here, the column is full
            return false;
        }

        /// <summary>
        /// Method to check for the win of the most recent player
        /// </summary>
        /// <param name="col">The column of the placed disk</param>
        /// <param name="row">The row of the placed disk</param>
        /// <returns>True if the player has 4 connected disks in any direction and otherwise false</returns>
        private bool CheckWin(int col, int row)
        {
            // return false if no disk at the specified location
            if (_board[col, row] == null) return false;
            
            // Get the player number of the disk at the current position
            int player = _board[col, row].PlayerNumber;
    
            // Check horizontal (left and right)
            if (CountInDirection(col, row, -1, 0, player) + CountInDirection(col, row, 1, 0, player) >= 3)
                return true;
    
            // Check vertical (only downward since disks stack upward)
            if (CountInDirection(col, row, 0, 1, player) >= 3)
                return true;
    
            // Check diagonal (top-left to bottom-right)
            if (CountInDirection(col, row, -1, -1, player) + CountInDirection(col, row, 1, 1, player) >= 3)
                return true;
    
            // Check diagonal (top-right to bottom-left)
            if (CountInDirection(col, row, 1, -1, player) + CountInDirection(col, row, -1, 1, player) >= 3)
                return true;
    
            // If no direction resulted in 4 in a row return false
            return false;
        }

        /// <summary>
        /// Counts how many consecutive disks belong to the same player
        /// </summary>
        /// <param name="startCol">Column of the last disk that was placed</param>
        /// <param name="startRow">Row of the last disk that was placed</param>
        /// <param name="colStep">Direction to move in the column axis</param>
        /// <param name="rowStep">Direction to move in the row axis</param>
        /// <param name="player">The player number whose disks we are checking for</param>
        /// <returns>The number of consecutive disks matching the player's disk in the given direction</returns>
        private int CountInDirection(int startCol, int startRow, int colStep, int rowStep, int player)
        {
            int count = 0;
            int col = startCol + colStep;
            int row = startRow + rowStep;
    
            while (col >= 0 && col < COLS && row >= 0 && row < ROWS && 
                   _board[col, row] != null && _board[col, row].PlayerNumber == player)
            {
                count++;
                col += colStep;
                row += rowStep;
            }
            return count;
        }

        /// <summary>
        /// Resets the game state to start a new game
        /// Clears the board and resets the disk counts as well as removes the winner
        /// </summary>
        public void ResetGame()
        {
           GameOver = false;
           Winner = null;
           _board = new Disk[COLS, ROWS];
           _disksLeft1 = 21;
           _disksLeft2 = 21;
        }
    }
}