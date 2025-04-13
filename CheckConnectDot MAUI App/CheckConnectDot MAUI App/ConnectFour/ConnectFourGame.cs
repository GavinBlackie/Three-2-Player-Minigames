namespace CheckConnectDot_MAUI_App.ConnectFour
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

        public bool GameOver { get; private set; }
        public Player? Winner { get; private set; }

        public ConnectFourGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _players = new Player[] { playersTuple.Item1, playersTuple.Item2 };
            _currentPlayer = _players[0];
            _board = new Disk[COLS, ROWS];
            _disksLeft1 = 21;
            _disksLeft2 = 21;
        }

        public Player CurrentPlayer { get { return _currentPlayer; } }

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

        private bool CheckWin(int col, int row)
        {
            if (_board[col, row] == null) return false;
            
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
    
            return false;
        }

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

        public void ResetGame()
        {
           
        }
    }
}