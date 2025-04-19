using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    /* Author: Joseph Thomas */

    /// <summary>
    /// Represents a square in the Dots and Boxes game grid.
    /// Also contains four Line objects representing its sides and tracks ownership.
    /// </summary>
    public class Box
    {
        #region Fields
        /// <summary>
        /// The top line of the box
        /// </summary>
        private Line _top;

        /// <summary>
        /// The bottom line of the box
        /// </summary>
        private Line _bottom;

        /// <summary>
        /// The left line of the box
        /// </summary>
        private Line _left;

        /// <summary>
        /// The right line of the box
        /// </summary>
        private Line _right;
        

        /// <summary>
        /// Which player captured this box
        /// </summary>
        private DotsandBoxesGameState _team;
        
        /// <summary>
        /// Row position on the grid
        /// </summary>
        private int  _row;

        /// <summary>
        /// Column position on the grid
        /// </summary>
        private int _column;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that creates a new Box instance with specified lines and team
        /// </summary>
        /// <param name="top">Top line</param>
        /// <param name="bottom">Bottom line line</param>
        /// <param name="left">Left line line</param>
        /// <param name="right">Right boundary line</param>
        /// <param name="team">Capturer</param>
        public Box(Line top, Line bottom, Line left, Line right, DotsandBoxesGameState team)
        {
            _top = top;
            _bottom = bottom;
            _left = left;
            _right = right;
            _team = team;
            _row = _top.Y1;
            _column = _left.X1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the top line of the box
        /// </summary>
        public Line Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the bottom line of the box
        /// </summary>
        public Line Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the left line of the box
        /// </summary>
        public Line Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets the right line of the box
        /// </summary>
        public Line Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets/sets which player owns this box
        /// </summary>
        public DotsandBoxesGameState Team
        {
            get { return _team; }
            set { _team = value; }
        }

        /// <summary>
        /// Gets/sets the box's row position in the grid
        /// </summary>
        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        /// <summary>
        /// Gets/sets the box's column position in the grid
        /// </summary>
        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Determines if two Box instances are equal by comparing their positions and lines
        /// </summary>
        /// <param name="obj">The Box to compare with</param>
        /// <returns>True if boxes have same position and lines</returns>
        public override bool Equals(object obj)
        {
            if (obj is Box other)
            {
                return Row == other.Row &&
                       Column == other.Column &&
                       Top.Equals(other.Top) &&
                       Bottom.Equals(other.Bottom) &&
                       Left.Equals(other.Left) &&
                       Right.Equals(other.Right);
            }
            return false;
        }

        /// <summary>
        /// Generates a hash code based on the box's position and lines
        /// </summary>
        /// <returns>Unique hash code for this box</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column, Top, Bottom, Left, Right);
        }

        /// <summary>
        /// Returns a string representation of the box's lines
        /// </summary>9
        /// <returns>Formatted string showing all four lines</returns>
        public override string ToString()
        {
            return $"Top: {_top}, Bottom: {_bottom}, Left: {_left}, Right: {_right}";
        }
    }

    #endregion
}
