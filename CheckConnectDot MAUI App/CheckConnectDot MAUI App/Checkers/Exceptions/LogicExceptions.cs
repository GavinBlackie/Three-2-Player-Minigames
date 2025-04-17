/*
 * Author: Gavin Blackie
 * File Description: 
 *      LogicExceptions is for any logical exception definitions that are related to the checkers game
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers.Exceptions
{
    /// <summary>
    /// Custom exception specifically to raise attention to exceptional cases in a checkers game
    /// </summary>
    public class CheckersPieceException : Exception
    {
        /// <summary>
        /// A possible contained piece in this exception
        /// </summary>
        private Piece? _piece;

        /// <summary>
        /// Constructor for a new CheckersPiece exception. Only needs a string message, but
        /// can also welcome a piece instance if required.
        /// </summary>
        /// <param name="message"> The error message </param>
        /// <param name="piece"> The related piece instance </param>
        public CheckersPieceException(string message, Piece? piece = null) : base(message)
        {
            _piece = piece;
        }

        /// <summary>
        /// Readonly property for the contained piece in this CheckersPieceException
        /// </summary>
        public Piece? Piece
        {
            get
            {
                return _piece;
            }
        }
    }

    /// <summary>
    /// Custom exception for when a checkers piece cannot move
    /// </summary>
    public class InvalidPieceMove : CheckersPieceException
    {
        public InvalidPieceMove(string message, Piece? piece = null) : base(message, piece)
        {
        }
    }

    /// <summary>
    /// Custom exception for when a checkers piece cannot capture
    /// </summary>
    public class InvalidCaptureMove : CheckersPieceException
    {
        public InvalidCaptureMove(string message, Piece? piece = null) : base(message, piece)
        {
        }
    }
}
