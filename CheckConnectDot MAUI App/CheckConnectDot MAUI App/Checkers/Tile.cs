using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers
{
    internal class Tile
    {
        #region Fields

        private Piece? _piece;

        private ImageButton _imgBtnTile;

        #endregion

        #region Constructors
        
        internal Tile(ImageButton imgBtnTile)
        {
            _imgBtnTile = imgBtnTile;
            _piece = null; // Initially a tile does not have a piece on it
        }

        #endregion


        #region Properties

        internal Piece Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
            }
        }

        internal ImageButton ImgButton
        {
            get
            {
                return _imgBtnTile;
            }
            set
            {
                _imgBtnTile = value;
            }
        }

        #endregion
    }
}
