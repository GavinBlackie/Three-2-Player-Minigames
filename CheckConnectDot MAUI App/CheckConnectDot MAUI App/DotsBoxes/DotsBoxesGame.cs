using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;


namespace CheckConnectDot_MAUI_App.DotsBoxes
{
    /* Author: Joseph Thomas */

    /// <summary>
    /// Manages the core game logic for Dots and Boxes, including:
    /// - Game state tracking (current player, scores), Line and box management,
    /// - Move validation, Win condition checking
    /// </summary>
    public class DotsBoxesGame : Game
    {
        #region Fields
        /// <summary>
        /// Stores all the lines drawn on the board
        /// </summary>
        private List<Line> _lines;    
        
        /// <summary>
        /// Stores all the boxes captured on the board
        /// </summary>
        private List<Box> _boxes;

        /// <summary>
        /// Tracks boxes captured by the blue player
        /// </summary>
        private List<Box> _blueBoxes;

        /// <summary>
        /// Tracks boxes captured by the red player
        /// </summary>
        private List<Box> _redBoxes;

        /// <summary>
        /// Tracks whos turn it is
        /// </summary>
        private DotsandBoxesGameState _gameState;
        /// <summary>
        /// Tracks if the game has ended
        /// </summary>
        private bool _isGameOver;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that initializes a new Dots and Boxes game instance
        /// </summary>
        /// <param name="playersTuple">Tuple containing the two players</param>
        public DotsBoxesGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _lines = new List<Line>();
            _boxes = new List<Box>();
            _blueBoxes = new List<Box>();
            _redBoxes = new List<Box>();
            _gameState = new DotsandBoxesGameState();
            _gameState = DotsandBoxesGameState.BluePlayerTurn;
            _isGameOver = false;
        }

        #endregion

        #region Properties
        public List<Line> Lines
        {
            get { return _lines; }
        }

        public List<Box> Boxes
        {
            get { return _boxes; }
        }

        public DotsandBoxesGameState GameState
        {
            get { return _gameState; }
        }

        public bool IsGameOver
        {
            get { return _isGameOver; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes a player's move and updates game state
        /// </summary>
        /// <param name="line">The line being drawn</param>
        public void MakeMove(Line line)
        {
            _lines.Add(line);  // Add the new line to the list
            List<Box> newlyCompletedBoxes = new();

            // Store current player before checking for box completions
            var currentPlayerBeforeMove = _gameState;

            // Check all boxes for completion
            foreach (var box in _boxes)
            {
                if (box.Team == DotsandBoxesGameState.None && IsBoxComplete(box))
                {
                    // Claim box for current player
                    box.Team = currentPlayerBeforeMove;
                    newlyCompletedBoxes.Add(box);
                }
            }

            if (newlyCompletedBoxes.Count > 0)
            {
                // Process completed boxes
                foreach (var box in newlyCompletedBoxes)
                {
                    // Adds boxes based on which players turn it was
                    if (currentPlayerBeforeMove == DotsandBoxesGameState.BluePlayerTurn)
                    {
                        _blueBoxes.Add(box);
                    }
                    else
                    {
                        _redBoxes.Add(box);
                    }
                }

                // Check for game end
                if (_boxes.Count == 16)
                {
                    _isGameOver = true;
                }

                // Player can go again
            }
            else
            {
                // Switch turns if no boxes were completed
                SwitchPlayer(); 
            }
        }

        /// <summary>
        /// Creates a new line between two points
        /// </summary>
        /// <param name="x1,y1">Starting X and Ycoordinates</param>
        /// <param name="x2,y2">Ending X and Ycoordinates</param>
        /// <returns>The created Line object</returns>
        public Line CreateLine(int x1, int y1, int x2, int y2)
        {
            return new Line((byte)x1, (byte)y1, (byte)x2, (byte)y2, _gameState);
        }

        /// <summary>
        /// Checks if a proposed line is valid (not already drawn)
        /// </summary>
        /// <param name="line">Line to validate</param>
        /// <returns>True if the move is valid</returns>
        public bool IsValidMove(Line line)
        {
            return !_lines.Any(l =>
                l.X1 == line.X1 && l.Y1 == line.Y1 &&
                l.X2 == line.X2 && l.Y2 == line.Y2);
        }

        /// <summary>
        /// Determines if a box is completely enclosed by lines
        /// </summary>
        /// <param name="box">The box to check</param>
        /// <returns>True if all sides have been drawn</returns>
        private bool IsBoxComplete(Box box)
        {
            bool hasTop = _lines.Contains(box.Top);
            bool hasBottom = _lines.Contains(box.Bottom);
            bool hasLeft = _lines.Contains(box.Left);
            bool hasRight = _lines.Contains(box.Right);

            return hasTop && hasBottom && hasLeft && hasRight;
        }

        /// <summary>
        /// Gets the current player's color
        /// </summary>
        /// <returns>Color representing current player</returns>
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

        /// <summary>
        /// Switches turns between players
        /// </summary>
        public void SwitchPlayer()
        {
            if (_gameState == DotsandBoxesGameState.BluePlayerTurn)
            {
                _gameState = DotsandBoxesGameState.RedPlayerTurn;
            }
            else
            {
                _gameState = DotsandBoxesGameState.BluePlayerTurn;
            }
        }

        /// <summary>
        /// Marks a box as completed
        /// </summary>
        /// <param name="image">The Image control representing the box</param>
        public void MarkBoxAsCompleted(Image image)
        {
            if (_gameState == DotsandBoxesGameState.BluePlayerTurn)
            {
                image.BackgroundColor = Colors.Blue;
            }
            else if (_gameState == DotsandBoxesGameState.RedPlayerTurn)
            {
                image.BackgroundColor = Colors.Red;
            }
        }

        /// <summary>
        /// Determines the game winner and updates player stats
        /// </summary>
        /// <returns> The Winner announcement as a string</returns>
        public string GetWinner()
        {
            if (_blueBoxes.Count > _redBoxes.Count)
            {
                _playerTuple.Item1.NumWins++;
                return "Blue Wins!";
            }
            else if (_blueBoxes.Count < _redBoxes.Count)
            {
                _playerTuple.Item2.NumWins++;
                return "Red Wins!";
            }
            else
            {
                return "It's a Tie";
            }
        }
    }

    #endregion
}
