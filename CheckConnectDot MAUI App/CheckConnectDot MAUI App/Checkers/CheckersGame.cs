using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers
{
    public class CheckersGame : Game
    {
        #region Fields
        private List<Piece> _pieceList;

        /// <summary>
        /// Array of tiles representing each tile on the board
        /// </summary>
        private Tile[] _tilesArr;

        private (string team1, string team2) _teamNames;

        private CheckersGameState _gameState;
        #endregion

        #region Constructors
        public CheckersGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _pieceList = new List<Piece>();
            _tilesArr = new Tile[64];
            _teamNames = ("Blue", "Red");
            _gameState = CheckersGameState.BlueTurn;

            CreateTeamDefaultPieces(_teamNames.team1);
            CreateTeamDefaultPieces(_teamNames.team2, 5, 1);
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

        internal Tile[] Tiles
        {
            get
            {
                return _tilesArr;
            }
            set
            {
                _tilesArr = value;
            }
        }

        #endregion

        #region Methods

        private void CreateTeamDefaultPieces(string team, int yOffset=0, int rowShiftConditional=0)
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

        #endregion
    }
}
