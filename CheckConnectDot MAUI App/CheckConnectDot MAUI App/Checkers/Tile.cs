using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Layouts;

namespace CheckConnectDot_MAUI_App.Checkers
{
    internal class Tile
    {
        #region Fields

        private (int, int) _pos;

        private Piece? _piece;

        #endregion

        #region Constructors
        
        internal Tile((int, int) pos)
        {
            _pos = pos;
            _piece = null; // Initially a tile does not have a piece on it
        }

        #endregion


        #region Properties

        internal Piece? Piece
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

        internal (int xPos, int yPos) Position
        {
            get
            {
                return _pos;
            }
            set
            {
                _pos = value;
            }
        }

        #endregion
    }
}
