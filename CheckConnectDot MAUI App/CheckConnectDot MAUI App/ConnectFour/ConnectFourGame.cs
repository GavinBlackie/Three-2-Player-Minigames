namespace CheckConnectDot_MAUI_App.ConnectFour
{
    public class ConnectFourGame : Game
    {
        private Player _currentPlayer;
        private Player[] _players;
        private int[,] _board;
        private int _disksLeft1;
        private int _disksLeft2;

        private const string RED_DISK_DIR = "player1_disk.png";
        private const string BLUE_DISK_DIR = "player2_disk.png";

        
        public ConnectFourGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _players = new Player[] { playersTuple.Item1, playersTuple.Item2 };
            _currentPlayer = _players[0]; // Player 1 starts
            _board = new int[7, 6]; // 7 columns, 6 rows
            _disksLeft1 = 21;
            _disksLeft2 = 21;
        }

        public Player CurrentPlayer { get { return _currentPlayer; } }
        public int DisksLeft1 { get { return _disksLeft1; } }
        public int DisksLeft2 { get { return _disksLeft2; } }

        public void DropDisk(int column)
        {
            
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
