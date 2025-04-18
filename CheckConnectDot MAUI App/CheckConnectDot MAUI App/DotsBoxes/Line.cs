using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    public class Line
    {
        private int _x1;
        private int _y1;
        private int _x2;
        private int _y2;

        private DotsandBoxesGameState _team;
        private bool _isVertical;
        private bool _isHorizontal;

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

        public int X1
        {
            get { return _x1; }
        }

        public int Y1
        {
            get { return _y1; }
        }

        public int X2
        {
            get { return _x2; }
        }

        public int Y2
        {
            get { return _y2; }
        }

        public DotsandBoxesGameState Team
        { 
            get { return _team; } 
        }

        public bool IsVertical
        {
            get { return _isVertical; }
        }

        public bool IsHorizontal
        {
            get { return _isHorizontal; }
        }

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


        public override bool Equals(object? obj)
        {
            if (obj is not Line other) return false;

            // Normalize both lines for comparison
            (int ax1, int ay1, int ax2, int ay2) = NormalizeCoordinates(this._x1, this._y1, this._x2, this._y2);
            (int bx1, int by1, int bx2, int by2) = NormalizeCoordinates(other._x1, other._y1, other._x2, other._y2);

            return ax1 == bx1 && ay1 == by1 &&
                   ax2 == bx2 && ay2 == by2;
        }

        public override int GetHashCode()
        {
            (int nx1, int ny1, int nx2, int ny2) = NormalizeCoordinates(_x1, _y1, _x2, _y2);
            return HashCode.Combine(nx1, ny1, nx2, ny2);
        }

        public override string ToString()
        {
            return $"X1: {_x1}, Y1: {_y1}, X2: {_x2}, Y2: {_y2}";
        }

    }
}
