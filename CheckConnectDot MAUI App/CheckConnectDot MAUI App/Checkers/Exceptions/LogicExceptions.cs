using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers.Exceptions
{
    public class CheckersPieceException : Exception
    {
        private Piece? _piece;

        public CheckersPieceException(string message, Piece? piece = null) : base(message)
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

    public class InvalidPieceMove : CheckersPieceException
    {
        public InvalidPieceMove(string message, Piece? piece = null) : base(message, piece)
        {
        }
    }

    public class InvalidCaptureMove : CheckersPieceException
    {
        public InvalidCaptureMove(string message, Piece? piece = null) : base(message, piece)
        {
        }
    }
}
