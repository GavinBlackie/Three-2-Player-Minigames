using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    /* Author: Joseph Thomas */

    //// <summary>
    /// Represents a line on the Dots and Boxes game grid.
    /// </summary>
    public class Line
    {
        #region Fields
        /// <summary>
        /// Int representing the first X coordinate
        /// </summary>
        private int _x1;

        /// <summary>
        /// Int representing the first Y coordinate
        /// </summary>
        private int _y1;

        /// <summary>
        /// Int representing the second X coordinate
        /// </summary>
        private int _x2;

        /// <summary>
        /// Int representing the second Y coordinate
        /// </summary>
        private int _y2;

        /// <summary>
        /// Which player drew this box
        /// </summary>
        private DotsandBoxesGameState _team;

        /// <summary>
        /// If the line is vertical
        /// </summary>
        private bool _isVertical;

        /// <summary>
        /// If the line is horizontal
        /// </summary>
        private bool _isHorizontal;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that creates a new line instance with specified X and Y coords and Team
        /// </summary>
        /// <param name="x1">Initial X coord</param>
        /// <param name="y1">Initial Y coord</param>
        /// <param name="x2">Final X coord</param>
        /// <param name="y2">Final X coord</param>
        /// <param name="team">Team that drew it</param>
        public Line(int x1, int y1, int x2, int y2, DotsandBoxesGameState team)
        {
            _x1 = x1 / 2;
            _y1 = y1 / 2;
            _x2 = x2 / 2;
            _y2 = y2 / 2;
            _team = team;

            _isVertical = _x1 == _x2;
            _isHorizontal = _y1 == _y2;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the first X coord of the box
        /// </summary>
        public int X1
        {
            get { return _x1; }
        }

        /// <summary>
        /// Gets the first Y coord of the box
        /// </summary>
        public int Y1
        {
            get { return _y1; }
        }

        /// <summary>
        /// Gets the last X coord of the box
        /// </summary>
        public int X2
        {
            get { return _x2; }
        }

        /// <summary>
        /// Gets the last Y coord of the box
        /// </summary>
        public int Y2
        {
            get { return _y2; }
        }

        /// <summary>
        /// Gets the Team that drew the line
        /// </summary>
        public DotsandBoxesGameState Team
        { 
            get { return _team; } 
        }

        /// <summary>
        /// Gets whether or not the line is vertical
        /// </summary>
        public bool IsVertical
        {
            get { return _isVertical; }
        }

        /// <summary>
        /// Gets whether or not the line is horizontal
        /// </summary>
        public bool IsHorizontal
        {
            get { return _isHorizontal; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Normalizes the coordinates of the line so equalty is easier to check
        /// </summary>
        /// <param name="x1">First X coord</param>
        /// <param name="y1">First Y coord</param>
        /// <param name="x2">Last X coord</param>
        /// <param name="y2">Last Y coord</param>
        /// <returns></returns>
        private static (int x1, int y1, int x2, int y2) NormalizeCoordinates(int x1, int y1, int x2, int y2)
        {
            if (x1 < x2 || (x1 == x2 && y1 < y2))
            {
                return (x1, y1, x2, y2);
            }
            else
            {
                return (x2, y2, x1, y1);
            }
        }

        /// <summary>
        /// Determines if two line instances are equal by comparing their coordinates
        /// </summary>
        /// <param name="obj">The line to compare with</param>
        /// <returns>True if line have same coordinates</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Line other) return false;

            (int ax1, int ay1, int ax2, int ay2) = NormalizeCoordinates(this._x1, this._y1, this._x2, this._y2);
            (int bx1, int by1, int bx2, int by2) = NormalizeCoordinates(other._x1, other._y1, other._x2, other._y2);

            return ax1 == bx1 && ay1 == by1 &&
                   ax2 == bx2 && ay2 == by2;
        }

        /// <summary>
        /// Generates a hash code based on the lines coordinates
        /// </summary>
        /// <returns>Unique hash code for this line</returns>
        public override int GetHashCode()
        {
            (int nx1, int ny1, int nx2, int ny2) = NormalizeCoordinates(_x1, _y1, _x2, _y2);
            return HashCode.Combine(nx1, ny1, nx2, ny2);
        }

        /// <summary>
        /// Returns a string representation of the line
        /// </summary>9
        /// <returns>Formatted string showing all four coordinates</returns>
        public override string ToString()
        {
            return $"X1: {_x1}, Y1: {_y1}, X2: {_x2}, Y2: {_y2}";
        }

    }

    #endregion
}
