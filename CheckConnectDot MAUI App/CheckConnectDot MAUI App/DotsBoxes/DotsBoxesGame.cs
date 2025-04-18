using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;


namespace CheckConnectDot_MAUI_App.DotsBoxes
{
    public class DotsBoxesGame : Game
    {
        private List<Line> _lines = new();
        private List<Box> _boxes = new();

        private List<Box> _blueBoxes;
        private List<Box> _redBoxes;

        private DotsandBoxesGameState _gameState;
        private int _blueScore;
        private int _redScore;
        private Color _currentPlayerColor;
        private bool _isGameOver;
        private int _gridSize;

        public DotsBoxesGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _gameState = new DotsandBoxesGameState();
            _gameState = DotsandBoxesGameState.BluePlayerTurn;
            _blueScore = 0;
            _redScore = 0;
            _isGameOver = false;
            _currentPlayerColor = Colors.Blue;
            _blueBoxes = new List<Box>();
            _redBoxes = new List<Box>();

            _gridSize = 4;

        }

        public DotsandBoxesGameState GameState
        {
            get { return _gameState; }
        }

        public List<Line> Lines
        {
            get { return _lines; }
        }

        public List<Box> Boxes
        {
            get { return _boxes; }
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


        public void MakeMove(Line line)
        {
            _lines.Add(line);
            List<Box> newlyCompletedBoxes = new();

            // Store current player before any potential switch
            var currentPlayerBeforeMove = _gameState;

            foreach (var box in _boxes)
            {
                if (box.Team == DotsandBoxesGameState.None && IsBoxComplete(box))
                {
                    box.Team = currentPlayerBeforeMove; 
                    newlyCompletedBoxes.Add(box);
                }
            }

            if (newlyCompletedBoxes.Count > 0)
            {
                foreach (var box in newlyCompletedBoxes)
                {
                    if (currentPlayerBeforeMove == DotsandBoxesGameState.BluePlayerTurn)
                    {
                        _blueBoxes.Add(box);
                        _blueScore++;
                    }
                    else
                    {
                        _redBoxes.Add(box);
                        _redScore++;
                    }
                }
                // Player keeps turn if a box was caputured
            }
            else
            {
                SwitchPlayer(); // Only switch if no boxes were captured
            }
        }

        public Line CreateLine(int x1, int y1, int x2, int y2)
        {
            return new Line((byte)x1, (byte)y1, (byte)x2, (byte)y2, _gameState);
        }

        public bool IsValidMove(Line line)
        {
            return !_lines.Any(l =>
                l.X1 == line.X1 && l.Y1 == line.Y1 &&
                l.X2 == line.X2 && l.Y2 == line.Y2);
        }

        private bool IsBoxComplete(Box box)
        {
            bool hasTop = _lines.Contains(box.Top);
            bool hasBottom = _lines.Contains(box.Bottom);
            bool hasLeft = _lines.Contains(box.Left);
            bool hasRight = _lines.Contains(box.Right);

            return hasTop && hasBottom && hasLeft && hasRight;
        }


        public Color GetPlayerColor()
        {
            if (_gameState == DotsandBoxesGameState.BluePlayerTurn)
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

        public void SwitchPlayer()
        {
            if (_gameState == DotsandBoxesGameState.BluePlayerTurn)
            {
                _gameState = DotsandBoxesGameState.RedPlayerTurn;
                _currentPlayerColor = Colors.Red;
            }
            else
            {
                _gameState = DotsandBoxesGameState.BluePlayerTurn;
                _currentPlayerColor = Colors.Blue;
            }
        }


        public void MarkBoxAsCompleted(Image image)
        {
            if (_gameState == DotsandBoxesGameState.BluePlayerTurn)
            {
                image.BackgroundColor = Colors.Blue;
                _blueScore += 1;
            }
            else if (_gameState == DotsandBoxesGameState.RedPlayerTurn)
            {
                image.BackgroundColor = Colors.Red;
                _redScore += 1;
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
