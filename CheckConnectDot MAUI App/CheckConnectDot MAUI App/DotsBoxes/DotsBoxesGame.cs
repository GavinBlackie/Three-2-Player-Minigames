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

            foreach (var box in _boxes)
            {
                if (box.Team == DotsandBoxesGameState.None && IsBoxComplete(box))
                {
                    box.Team = _gameState;
                    newlyCompletedBoxes.Add(box);
                }
            }

            if (newlyCompletedBoxes.Count > 0)
            {
                foreach (var box in newlyCompletedBoxes)
                {
                    if (_gameState == DotsandBoxesGameState.BluePlayerTurn)
                    {
                        _blueBoxes.Add(box);
                        _blueScore++;
                    }
                    else
                    {
                        _redBoxes.Add(box);
                        _redScore++;
                    }
                    //UpdateBoxAppearance(box); // Ensure this method updates the UI
                }
            }
            else
            {
                SwitchPlayer();
            }
        }

        //public List<Line> CompleteBox(Line line)
        //{
        //    List<Line> completedLines = new List<Line>();

        //    if (line.IsHorizontal)
        //    {
        //        // Box ABOVE
        //        if (line.Y1 > 0)
        //        {
        //            completedLines.AddRange(CreateBoxLines(line, line.Y1 - 1));
        //        }

        //        // Box BELOW
        //        if (line.Y1 < _gridSize - 1)
        //        {
        //            completedLines.AddRange(CreateBoxLines(line, line.Y1 + 1));
        //        }
        //    }
        //    else // Vertical
        //    {
        //        // Box to the LEFT
        //        if (line.X1 > 0)
        //        {
        //            completedLines.AddRange(CreateBoxLines(line, line.X1 - 1));
        //        }

        //        // Box to the RIGHT
        //        if (line.X1 < _gridSize - 1)
        //        {
        //            completedLines.AddRange(CreateBoxLines(line, line.X1 + 1));
        //        }
        //    }

        //    return completedLines;
        //}

        //private List<Line> CreateBoxLines(Line line, int offset)
        //{
        //    List<Line> boxLines = new List<Line>();

        //    if (line.IsHorizontal)
        //    {
        //        boxLines.Add(new Line(line.X1, offset, line.X2, offset, line.Team)); // Top or Bottom line
        //        boxLines.Add(new Line(line.X1, offset + 1, line.X2, offset + 1, line.Team)); // Opposite line
        //        boxLines.Add(new Line(line.X1, offset, line.X1, offset + 1, line.Team)); // Left line
        //        boxLines.Add(new Line(line.X2, offset, line.X2, offset + 1, line.Team)); // Right line
        //    }
        //    else
        //    {
        //        boxLines.Add(new Line(offset, line.Y1, offset, line.Y2, line.Team)); // Left or Right line
        //        boxLines.Add(new Line(offset + 1, line.Y1, offset + 1, line.Y2, line.Team)); // Opposite line
        //        boxLines.Add(new Line(offset, line.Y1, offset + 1, line.Y1, line.Team)); // Top line
        //        boxLines.Add(new Line(offset, line.Y2, offset + 1, line.Y2, line.Team)); // Bottom line
        //    }

        //    return boxLines;
        //}

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

        //private void UpdateBoxAppearance(Box box)
        //{
        //    // Ensure that the box color is updated on the UI to reflect the player's color
        //    Color color = box.Team == DotsandBoxesGameState.BluePlayerTurn ? Colors.Blue :
        //                  box.Team == DotsandBoxesGameState.RedPlayerTurn ? Colors.Red :
        //                  Colors.Gray;

        //    // Update UI or game state to show the completed box with the correct color
        //    // You can update the BoxView here or similar UI elements
        //}



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

        public void MarkBoxAsCompleted(Image image)
        {
            // Logic to mark the box as completed and update player scores or status
            // For example:
            if (_currentPlayerColor == Colors.Blue)
            {
                image.BackgroundColor = Colors.Blue;
                _blueScore += 1;
            }
            else if (_currentPlayerColor == Colors.Red)
            {
                image.BackgroundColor = Colors.Red;
                _redScore += 1;
            }
            else
            {

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
