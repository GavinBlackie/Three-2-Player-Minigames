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
            if (x1 < x2 || (x1 == x2 && y1 < y2))
            {
                _x1 = x1; _y1 = y1; _x2 = x2; _y2 = y2;
            }
            else
            {
                _x1 = x2; _y1 = y2; _x2 = x1; _y2 = y1;
            }
            _team = team;

            _isVertical = IsLineVertical();
            _isHorizontal = IsLineHorizontal();
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

        private bool IsLineVertical()
        {
            if (_x1 == _x2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsLineHorizontal()
        {
            if (_y1 == _y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Line other)
            {
                return _x1 == other._x1 &&
                       _y1 == other._y1 &&
                       _x2 == other._x2 &&
                       _y2 == other._y2;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x1, _y1, _x2, _y2);
        }

        public override string ToString()
        {
            return $"X1: {_x1}, Y1: {_y1}, X2: {_x2}, Y2: {_y2}";
        }

    }
}
