using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    public class Box
    {
        private Line _top;
        private Line _bottom;
        private Line _left;
        private Line _right;
        private DotsandBoxesGameState _team;


        public Box(Line top, Line bottom, Line left, Line right, DotsandBoxesGameState team)
        {
            _top = top;
            _bottom = bottom;
            _left = left;
            _right = right;
            _team = team;

        }

        public int GridRow => Math.Min(Top.Y1, Bottom.Y1) + 1;
        public int GridCol => Math.Min(Left.X1, Right.X1);

        public Line Top
        {
            get { return _top; }
        }

        public Line Bottom
        {
            get { return _bottom; }
        }

        public Line Left
        {
            get { return _left; }
        }

        public Line Right
        {
            get { return _right; }
        }
        public DotsandBoxesGameState Team
        {
            get { return _team; }
        }

        public override bool Equals(object obj)
        {
            if (obj is Box other)
            {
                return _top.X1 == other.Top.X1 && _top.Y1 == other.Top.Y1 &&
                       _bottom.X1 == other.Bottom.X1 && _bottom.Y1 == other.Bottom.Y1 &&
                       _left.X1 == other.Left.X1 && _left.Y1 == other.Left.Y1 &&
                       _right.X1 == other.Right.X1 && _right.Y1 == other.Right.Y1;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_top.X1, _top.Y1, _bottom.X1, _bottom.Y1,
                                    _left.X1, _left.Y1, _right.X1, _right.Y1);
        }

        public override string ToString()
        {
            return $"Top: {_top}, Bottom: {_bottom}, Left: {_left}, Right: {_right}";
        }
    }
}
