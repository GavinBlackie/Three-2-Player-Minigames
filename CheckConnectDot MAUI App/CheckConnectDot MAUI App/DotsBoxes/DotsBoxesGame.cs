using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.DotsBoxes
{
    public class DotsBoxesGame : Game
    {
        public const string BLUE_LINE_DIR = "Blue";
        public const string RED_LINE_DIR = "Red";
        public const string CLAIMED_BOX_DIR = "Gray";

        private List<Line> _lineList = new();
        private List<Box> _boxList = new();
        private DotsandBoxesGameState _gameState;

        public DotsBoxesGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _lineList = new List<Line>();
            _boxList = new List<Box>();
            _gameState = new DotsandBoxesGameState();
        }

        public void StartGame()
        {
        }


        public void ConnectDots(Line line)
        {
        }

        public bool IsConnectionValid(Line line)
        {
            return false;
        }

        public void CreateBox()
        {
        }

        public bool IsGameOver()
        {
            return false;
        }


    }
}
