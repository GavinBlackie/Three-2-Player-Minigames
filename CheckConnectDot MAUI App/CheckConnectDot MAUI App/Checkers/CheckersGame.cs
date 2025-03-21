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

        private CheckersGameState _gameState;
        #endregion

        #region Constructors
        public CheckersGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {
            _pieceList = new List<Piece>();
            _tilesArr = new Tile[64];
            _gameState = CheckersGameState.BlueTurn;
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
    }
}
