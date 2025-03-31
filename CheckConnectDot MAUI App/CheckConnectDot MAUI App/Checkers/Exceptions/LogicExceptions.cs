using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers.Exceptions
{
    public class InvalidPieceMove : Exception
    {
        private Piece? _piece;

        public InvalidPieceMove(string message, Piece? piece = null) : base(message)
        {
            _piece = piece;
        }

        public Piece? Piece
        {
            get
            {
                return _piece;
            }
        }
    }
}
