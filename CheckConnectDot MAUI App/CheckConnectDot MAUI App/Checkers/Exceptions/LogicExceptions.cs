using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers.Exceptions
{
    public class InvalidPieceMove : Exception
    {
        public InvalidPieceMove(string message) : base(message)
        {

        }
    }
}
