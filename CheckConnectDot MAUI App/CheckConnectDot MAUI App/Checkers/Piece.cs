using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers
{
    public class Piece
    {
        #region Fields

        private (int xPos, int yPos) _pos;

        private Team _team;

        private bool _isKing;

        #endregion

        #region Constructors
        
        internal Piece((int, int) pos, Team team)
        {
            _pos = pos;
            _team = team;
            _isKing = false;
        }

        #endregion

        #region Properties

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

        internal Team Team
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
