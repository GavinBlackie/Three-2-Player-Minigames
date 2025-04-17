/*
 * Author: Gavin Blackie
 * File Description: 
 *      The piece module, defines the logical piece class and its functionality
*/

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

        /// <summary>
        /// The x and y of a piece
        /// </summary>
        private (int xPos, int yPos) _pos;

        /// <summary>
        /// The team of this piece instance
        /// </summary>
        private Team _team;

        /// <summary>
        /// A bool on whether this piece is a king or not
        /// </summary>
        private bool _isKing;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor for a new piece instance, takes in a position tuple and a team parameter.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="team"></param>
        internal Piece((int, int) pos, Team team)
        {
            _pos = pos;
            _team = team;
            _isKing = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Read-write property for this piece's position
        /// </summary>
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

        /// <summary>
        /// Read-write property for this piece's team
        /// </summary>
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

        /// <summary>
        /// Read-write proeprty for this piece's king status
        /// </summary>
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
