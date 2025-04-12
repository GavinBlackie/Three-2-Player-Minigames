namespace CheckConnectDot_MAUI_App.ConnectFour
{
    public class ConnectFourGame : Game
    {
        private Player _currentPlayer;
        private Player[] _players;
        private int[,] _board;
        private int _disksLeft1;
        private int _disksLeft2;
        private const int ROWS = 6;
        private const int COLS = 7;

        private const string RED_DISK_DIR = "player1_disk.png";
        private const string BLUE_DISK_DIR = "player2_disk.png";

        
        public ConnectFourGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _players = new Player[] { playersTuple.Item1, playersTuple.Item2 };
            _currentPlayer = _players[0]; // Player 1 starts
            _board = new int[COLS, ROWS]; // 7 columns, 6 rows
            _disksLeft1 = 21;
            _disksLeft2 = 21;
        }

        public Player CurrentPlayer { get { return _currentPlayer; } }
        public int DisksLeft1 { get { return _disksLeft1; } }
        public int DisksLeft2 { get { return _disksLeft2; } }

        public void DropDisk(int column, Action<int, int, string> updateCell)
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
                if (_board[col, row] == 0) // Check if empty
                {
                    // Place the disk
                    _board[col, row] = _currentPlayer.Number;
                    string imageSource = _currentPlayer.Number == 1 ? RED_DISK_DIR : BLUE_DISK_DIR;
                    
                    // Update the UI
                    updateCell(col, row, imageSource);
                    return;
                }
            }
            // If we get here, the column is full
            throw new Connect4Exception("Column is full");
        }

        public void ResetGame()
        {
            // Reset to initial state
        }

        protected bool IsGameOver()
        {
            // Check for 4 in a row, column, or diagonal
            return false;
        }
    }
}
