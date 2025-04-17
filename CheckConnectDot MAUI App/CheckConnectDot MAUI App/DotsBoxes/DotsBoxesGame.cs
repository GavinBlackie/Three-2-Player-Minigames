using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;

namespace CheckConnectDot_MAUI_App.DotsBoxes
{
    public class DotsBoxesGame : Game
    {
        private List<Line> _lines = new();
        private List<Box> _boxes = new();

        private DotsandBoxesGameState _gameState;
        private int _blueScore;
        private int _redScore;
        private Color _currentPlayerColor;
        private bool _isGameOver;

        public DotsBoxesGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _gameState = new DotsandBoxesGameState();
            _blueScore = 0;
            _redScore = 0;
            _isGameOver = false;
            _currentPlayerColor = Colors.Blue;

        }

        public DotsandBoxesGameState GameState
        {
            get { return _gameState; }
        }

        public List<Line> Lines
        {
            get { return _lines; }
        }

        public int BlueScore 
        {  
            get { return _blueScore; } 
            set { _blueScore = value; }
        }

        public int RedScore
        {
            get { return _redScore; }
            set { _redScore = value; }
        }

        public bool IsGameOver
        {
            get { return _isGameOver; }
        }


        public Line CreateLine(int x1, int y1, int x2, int y2)
        {
            return new Line((byte)x1, (byte)y1, (byte)x2, (byte)y2, _gameState);
        }

        public bool IsValidMove(Line line)
        {
            foreach (Line madeLine in _lines)
            {
                if (AreLinesEqual(line, madeLine))
                {
                    return false;
                }
            }
            return true;
        }

        private bool AreLinesEqual(Line a, Line b)
        {
            return a.X1 == b.X1 && a.Y1 == b.Y1 &&
                   a.X2 == b.X2 && a.Y2 == b.Y2;
        }

        public void MakeMove(Line line)
        {
            _lines.Add(line);

            var completedBoxes = CheckForCompletedBoxes(line);
            if (completedBoxes.Count == 0)
            {
                SwitchPlayer();
            }
            else
            {
                if (GameState == DotsandBoxesGameState.BluePlayerTurn)
                    BlueScore += completedBoxes.Count;
                else
                    RedScore += completedBoxes.Count;
            }
        }

        private List<Box> CheckForCompletedBoxes(Line newLine)
        {
            var boxes = new List<Box>();
            // Implementation to find completed boxes
            return boxes;
        }

        public Color GetPlayerColor()
        {
            if(_gameState == DotsandBoxesGameState.BluePlayerTurn)
            {
                return Colors.Blue;
            }
            else if (_gameState == DotsandBoxesGameState.RedPlayerTurn)
            {
                return Colors.Red;
            }
            else
            {
                return Colors.Gray;
            }
        }

        private void SwitchPlayer()
        {
            if( GameState == DotsandBoxesGameState.BluePlayerTurn)
            {
                _gameState = DotsandBoxesGameState.RedPlayerTurn;
            }
            else
            {
                _gameState = DotsandBoxesGameState.BluePlayerTurn;
            }
        }

        public string GetWinner()
        {
            if (_blueScore > RedScore)
            {
                return "Blue Wins!";
            }
            else if (_redScore > BlueScore) 
            {
                return "Red Wins!";
            }
            else
            {
                return "It's a Tie";
            }
        }
    }

}
