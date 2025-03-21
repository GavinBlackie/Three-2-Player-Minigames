using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers
{
    internal class Piece
    {
        #region Fields

        private (byte, byte) _pos;

        private string _team;

        private bool _isKing;

        #endregion

        #region Constructors
        
        internal Piece((byte, byte) pos, string team)
        {
            _pos = pos;
            _team = team;
            _isKing = false;
        }

        #endregion

        #region Properties

        internal (byte, byte) Position
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

        internal string Team
        {
            get
            {
                return _team;
            }
            set
            {
                _team = value;
            }
        }

        internal bool IsKing
        {
            get
            {
                return _isKing;
            }
            set
            {
                _isKing = value;
            }
        }

        #endregion
    }
}
