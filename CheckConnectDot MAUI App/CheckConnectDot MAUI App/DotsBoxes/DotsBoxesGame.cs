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
            _blueScore = 0;
            _redScore = 0;
            _isGameOver = false;
            _currentPlayerColor = Colors.Blue;
            _blueBoxes = new List<Box>();
            _redBoxes = new List<Box>();
            _gridSize = 5;

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
            return !_lines.Any(l =>
                l.X1 == line.X1 && l.Y1 == line.Y1 &&
                l.X2 == line.X2 && l.Y2 == line.Y2);
        }

        public void MakeMove(Line line)
        {
            var completedBoxes = CheckForCompletedBoxes(line);

            if (completedBoxes.Count == 0)
            {
                SwitchPlayer();
            }
            else
            {
                foreach (var box in completedBoxes)
                {
                    _boxes.Add(box);
                    if (_gameState == DotsandBoxesGameState.BluePlayerTurn)
                    {
                        _blueBoxes.Add(box);
                        BlueScore += completedBoxes.Count;
                    }
                    else
                    {
                        _redBoxes.Add(box);
                        RedScore += completedBoxes.Count;

                    }
                }

            }

        }

        public List<Box> CheckForCompletedBoxes(Line newLine)
        {
            var completedBoxes = new List<Box>();

            foreach (var potentialBox in GetPotentialBoxes(newLine))
            {
                Debug.WriteLine(potentialBox);
                if (IsBoxComplete(potentialBox))
                {
                    Box box = new Box(
                        potentialBox.Top,
                        potentialBox.Bottom,
                        potentialBox.Left,
                        potentialBox.Right,
                        _gameState  // Use current game state
                    );

                    Debug.WriteLine(box);

                    completedBoxes.Add(box);
                }
            }
            return completedBoxes;
        }


        private List<Box> GetPotentialBoxes(Line line)
        {
            var potentialBoxes = new List<Box>();

            if (line.IsHorizontal)
            {
                // Check for box above the horizontal line
                if (line.Y1 > 0)  // Ensure it's not out of bounds
                {
                    potentialBoxes.Add(new Box(
                        new Line(line.X1, line.Y1 - 1, line.X2, line.Y2 - 1, line.Team), // Top
                        line, // Bottom
                        new Line(line.X1, line.Y1 - 1, line.X1, line.Y1, line.Team), // Left
                        new Line(line.X2, line.Y2 - 1, line.X2, line.Y2, line.Team), // Right
                        line.Team
                    ));
                }

                // Check for box below the horizontal line
                if (line.Y1 < _gridSize - 1)  // Ensure it's not out of bounds
                {
                    potentialBoxes.Add(new Box(
                        line, // Top
                        new Line(line.X1, line.Y1 + 1, line.X2, line.Y2 + 1, line.Team), // Bottom
                        new Line(line.X1, line.Y1, line.X1, line.Y1 + 1, line.Team), // Left
                        new Line(line.X2, line.Y2, line.X2, line.Y2 + 1, line.Team), // Right
                        line.Team
                    ));
                }
            }
            else // Vertical line
            {
                // Check for box to the left of the vertical line
                if (line.X1 > 0)  // Ensure it's not out of bounds
                {
                    potentialBoxes.Add(new Box(
                        new Line(line.X1 - 1, line.Y1, line.X1, line.Y1, line.Team), // Top
                        new Line(line.X1 - 1, line.Y2, line.X1, line.Y2, line.Team), // Bottom
                        new Line(line.X1 - 1, line.Y1, line.X1 - 1, line.Y2, line.Team), // Left
                        line, // Right
                        line.Team
                    ));
                }

                // Check for box to the right of the vertical line
                if (line.X1 < _gridSize - 1)  // Ensure it's not out of bounds
                {
                    potentialBoxes.Add(new Box(
                        new Line(line.X1, line.Y1, line.X1 + 1, line.Y1, line.Team), // Top
                        new Line(line.X1, line.Y2, line.X1 + 1, line.Y2, line.Team), // Bottom
                        line, // Left
                        new Line(line.X1 + 1, line.Y1, line.X1 + 1, line.Y2, line.Team), // Right
                        line.Team
                    ));
                }
            }

            return potentialBoxes.Distinct().ToList();
        }



        private bool IsBoxComplete(Box box)
        {
            return _lines.Contains(box.Top) &&
                   _lines.Contains(box.Bottom) &&
                   _lines.Contains(box.Left) &&
                   _lines.Contains(box.Right);
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


        private void SwitchPlayer()
        {
            if (GameState == DotsandBoxesGameState.BluePlayerTurn)
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
