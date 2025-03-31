using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using CheckConnectDot_MAUI_App.Checkers.Exceptions;

namespace CheckConnectDot_MAUI_App.Checkers
{
    public class CheckersGame : Game
    {
        #region Fields

        /// <summary>
        /// List of pieces currently in this CheckersGame
        /// </summary>
        private List<Piece> _pieceList;

        /// <summary>
        /// Readonly tuple of strings representing the team names (can be compared to piece Team strings)
        /// </summary>
        private readonly (string team1, string team2) _teamNames = ("Blue", "Red");

        /// <summary>
        /// CheckersGameState value that determines what phase of the game it is currently in
        /// </summary>
        private CheckersGameState _gameState;

        /// <summary>
        /// Dictionary collection representing the mapped link between a MAUI ImageButton (visible tile), and its
        /// business logic counterpart - a Tile instance
        /// </summary>
        private Dictionary<ImageButton, Tile> _btnToTile;

        #endregion

        #region Constructors
        public CheckersGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _pieceList = new List<Piece>();

            _gameState = CheckersGameState.RedTurn;
            _btnToTile = new Dictionary<ImageButton, Tile>();

            // Generate all of the pieces in their default positions for both teams (account for some board shifting for the second team with integer literals)
            CreateTeamDefaultPieces(Team.Blue);
            CreateTeamDefaultPieces(Team.Red, 5, 1);
        }
        #endregion

        #region Properties
        internal List<Piece> Pieces
        {
            get
            {
                return _pieceList;
            }
            set
            {
                _pieceList = value;
            }
        }

        internal (string team1, string team2) TeamNames
        {
            get
            {
                return _teamNames;
            }
        }

        internal CheckersGameState GameState
        {
            get
            {
                return _gameState;
            }
            set
            {
                _gameState = value;
            }
        }

        internal Dictionary<ImageButton, Tile> BtnToTile
        {
            get
            {
                return _btnToTile;
            }
            set
            {
                _btnToTile = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates 12 default Pieces with a given team string name. Additionally has optional parameters for offsetting the initial y
        /// position of the generated pieces, and another that will "mirror" the way the checker pattern is generated depending on if it
        /// is equal to 0 or 1 (defaulted to 1).
        /// </summary>
        /// <param name="team">A string parameter for the team each piece is on</param>
        /// <param name="yOffset">The initial y offset position</param>
        /// <param name="rowShiftConditional">Special integer parameter that is either 0 or 1. Will change the row offset to "mirror" the initial piece x positions</param>
        private void CreateTeamDefaultPieces(Team team, int yOffset=0, int rowShiftConditional=0)
        {
            // Iterate three times, one for each row of pieces (each y/Row position)
            for (int iRow = 0; iRow < 3; iRow++)
            {
                // Account for the "shifting" of piece x postions every row
                int colShift = iRow % 2 == rowShiftConditional ? 1 : 0;

                // Iterate four times, one for each piece on a row (each x/Column position)
                for (int iColumn = 0; iColumn <= 7; iColumn+=2)
                {
                    // Create a new piece on that team accounting for the column shift
                    Piece piece = new Piece(((byte)(iColumn+colShift), (byte) (iRow+yOffset)), team);

                    // Add the piece to the pieceList collection
                    _pieceList.Add(piece);
                }
            }
        }

        internal Piece MovePiece(ref ImageButton initial, ref ImageButton destination)
        {
            // Step 1: Get the corresponding logical Tile and Piece instances
            Tile initialTile = _btnToTile[initial];
            Tile destTile = _btnToTile[destination];
            Piece? initialPiece = initialTile.Piece;
            Piece? destPiece = destTile.Piece;

            // Step 2: Validate the Tile instances (eg. ensure the initial has a piece, final does not have a piece)
            if (initialPiece is null)
            {
                throw new InvalidPieceMove("The intial tile did not have a logical piece to move", initialPiece);
            }
            if (destPiece is not null)
            {
                throw new InvalidPieceMove("The destination tile had a piece, could not move", initialPiece);
            }
            if ((int)initialPiece.Team != (int)_gameState)
            {
                throw new InvalidPieceMove("Piece clicked was of the wrong team", initialPiece);
            }

            // Step 3: Act - try to move the piece (get the tiles positions to reference in the movement)
            (int x, int y) initialPos = initialTile.Position;
            (int x, int y) destPos = destTile.Position;

            // Additionally, get int multipliers to help with calculations for non-king pieces
            int xMoveMultiplier = (initialPos.x < destPos.x) ? 1 : -1;
            int yMoveMultiplier = GetYMoveDirection();

            // If the direct distances between the tiles are 1, then move
            if (Math.Abs(destPos.x - initialPos.x) == 1 && Math.Abs(destPos.y - initialPos.y) == 1)
            {
                // Logically "move" the pieces by exchanging piece instances
                destTile.Piece = initialPiece;
                initialTile.Piece = null;

                // Adjust the Piece's logical position values
                destTile.Piece.Position = (destTile.Piece.Position.xPos + 1 * xMoveMultiplier, destTile.Piece.Position.yPos + 1 * yMoveMultiplier);

                ChangeTurn(); // Alternate the turn upon a successful move

                return destTile.Piece;
            }

            throw new InvalidPieceMove("All piece and tiles were valid, but a move could not be made", initialPiece);
        }

        internal void CapturePiece(ref ImageButton initial, ref ImageButton destination)
        {
            // Step 1: Get the corresponding logical Tile instances
            Tile initialTile = _btnToTile[initial];
            Tile destTile = _btnToTile[destination];

            // Additionally, get an int multiplier for directional movement calculations (for non-king pieces)
            int moveDir = GetYMoveDirection();

            // Step 2: Validate Tile instances & validate that there is a piece to capture
            if (initialTile.Piece is null)
            {
                throw new InvalidPieceMove("The intial tile did not have a logical piece to move", initialTile.Piece);
            }



            // Step 3: Act - try to capture a piece
        }

        private int GetYMoveDirection()
        {
            switch (_gameState)
            {
                case CheckersGameState.BlueTurn:
                    return 1;
                case CheckersGameState.RedTurn:
                    return -1;
                default:
                    Debug.Assert(false, "The game was not in an expected turn gamestate, defaulting to 1");
                    return 1;
            }
        }

        private void ChangeTurn()
        {
            switch (_gameState)
            {
                case CheckersGameState.RedTurn:
                    _gameState = CheckersGameState.BlueTurn;
                    break;
                case CheckersGameState.BlueTurn:
                    _gameState = CheckersGameState.RedTurn;
                    break;
                default:
                    Debug.Assert(false, "Tried to alternate turn on non-turn gamestate. Defaulting to red's turn.");
                    _gameState = CheckersGameState.RedTurn;
                    break;
            }
        }

        private void CapturePiece(Piece piece)
        {
            _pieceList.Remove(piece);
        }

        #endregion
    }
}
