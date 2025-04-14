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
            // Step 1: Get relevant information for a move

            // Get the corresponding logical Tile and Piece instances
            Tile initialTile = _btnToTile[initial];
            Tile destTile = _btnToTile[destination];
            Piece? initialPiece = initialTile.Piece;
            Piece? destPiece = destTile.Piece;
            // Next, Get the postions of the tiles
            (int x, int y) initialPos = initialTile.Position;
            (int x, int y) destPos = destTile.Position;
            // Additionally, get int multipliers to help with calculations for non-king pieces
            int xMoveMultiplier = (initialPos.x < destPos.x) ? 1 : -1;
            int yMoveMultiplier = GetYMoveDirection();

            // Step 2: Validate the Tile instances

            // Ensure the initial tile has a piece (cannot make a move of an invisible piece)
            if (initialPiece is null)
            {
                throw new InvalidPieceMove("The intial tile did not have a logical piece to move", initialPiece);
            }
            // Ensure that the destination tile does not have a piece
            if (destPiece is not null)
            {
                throw new InvalidPieceMove("The destination tile had a piece, could not move", initialPiece);
            }
            // Enure that the direct distances in X and Y are both 1
            if ((Math.Abs(destPos.x - initialPos.x) == 1 && Math.Abs(destPos.y - initialPos.y) == 1) == false)
            {
                throw new InvalidPieceMove("All piece and tiles were valid, but the move was not in the right spot", initialPiece);
            }
            // Ensure that the Y-direction the piece is moving is correct for its team (unless its a king)
            if (initialPiece.IsKing == false && 
                (initialPiece.Team == Team.Red && yMoveMultiplier == 1)
                ||
                (initialPiece.Team == Team.Blue && yMoveMultiplier == -1))
            {
                throw new InvalidPieceMove("Attempting to move backwards with a non-king piece", initialPiece);
            }

            // Step 3: Act - Move the piece (The move has been validated, now move)

            // First, logically adjust the position of the initial tile's piece
            initialPiece.Position = (initialPiece.Position.xPos + 1 * xMoveMultiplier, initialPiece.Position.yPos + 1 * yMoveMultiplier);

            // Then, "move" the pieces by exchanging piece instances
            destTile.Piece = initialPiece;
            initialTile.Piece = null;

            ChangeTurn(); // Alternate the turn upon a successful move
            return destTile.Piece;
        }

        internal Piece CapturePiece(ref ImageButton initial, ref ImageButton destination)
        {
            // Step 1: Get relevant information for a capture

            // Get the corresponding logical Tile and Piece instances
            Tile initialTile = _btnToTile[initial];
            Tile destTile = _btnToTile[destination];
            Piece? initialPiece = initialTile.Piece;
            Piece? destPiece = destTile.Piece;
            // Next, Get the postions of the tiles
            (int x, int y) initialPos = initialTile.Position;
            (int x, int y) destPos = destTile.Position;
            // Additionally, get int multipliers to help with calculations for non-king pieces
            int xMoveMultiplier = (initialPos.x < destPos.x) ? 1 : -1;
            int yMoveMultiplier = GetYMoveDirection();

            // Step 2: Validate the Tile instances (eg. ensure the initial has a piece, final does not have a piece)
            if (initialPiece is null)
            {
                throw new InvalidCaptureMove("The intial tile did not have a logical piece to move", initialPiece);
            }
            if (destPiece is not null)
            {
                throw new InvalidCaptureMove("The destination tile had a piece, could not move", initialPiece);
            }

            // Step 3: Act - try to move the piece (get the tiles positions to reference in the movement)

            // If the direct distances between the tiles are 2, then proceed to move and capture
            if (Math.Abs(destPos.x - initialPos.x) == 2 && Math.Abs(destPos.y - initialPos.y) == 2)
            {
                // First, validate the capture move (ensure that there is a piece to caputre)
                (Piece pieceCaptured, Tile captureTile) = ValidateCapture(initialTile, destTile, initialPiece.Team);
                CapturePiece(pieceCaptured, captureTile); // If the capture is valid (i.e an enemy piece exists in the capture tile, capture it)

                // Logically "move" the pieces by exchanging piece instances
                destTile.Piece = initialPiece;
                initialTile.Piece = null;
                // Then, adjust the Piece's logical position values
                destTile.Piece.Position = (destTile.Piece.Position.xPos + 2 * xMoveMultiplier, destTile.Piece.Position.yPos + 2 * yMoveMultiplier);

                ChangeTurn(); // Alternate the turn upon a successful move
                return destTile.Piece;
            }

            throw new InvalidCaptureMove("All pieces and tiles were valid, but a capture could not be made", initialPiece);
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

        private (Piece, Tile) ValidateCapture(Tile initial, Tile dest, Team attackingTeam)
        {
            // Save the positions of the two tiles for later
            (int x, int y) initialPos = initial.Position;
            (int x, int y) destPos = dest.Position;

            // Iterate through all tiles. Should one be between the initial and destination tiles and have a piece,
            // the capture is confirmed to be valid
            foreach (Tile capTile in _btnToTile.Values)
            {
                (int x, int y) capTilePos = capTile.Position;
                Piece? pieceCaptured = capTile.Piece;

                /*
                If the direct distance between the initial and destination tiles is 1 in each direction, the
                tile in question indeed has a piece, and the team of the piece is opposite, the move is valid
                */
                if ( Math.Abs(capTilePos.x - initialPos.x) == 1 && Math.Abs(capTilePos.y - initialPos.y) == 1
                    
                    && Math.Abs(capTilePos.x - destPos.x) == 1 && Math.Abs(capTilePos.y - destPos.y) == 1

                    && pieceCaptured is not null && pieceCaptured.Team != attackingTeam)
                {
                    return (pieceCaptured, capTile);
                }
            }

            // Should no piece be found with an enemy to be captured, then throw an InvalidCaptureMove
            throw new InvalidCaptureMove("No enemy piece was available to be captured!", initial.Piece);
        }

        private void CapturePiece(Piece piece, Tile capTile)
        {
            capTile.Piece = null;
            _pieceList.Remove(piece);

            foreach (ImageButton imageButton in _btnToTile.Keys)
            {
                if (_btnToTile[imageButton] == capTile)
                {
                    imageButton.Source = null;
                }
            }
        }

        #endregion
    }
}
