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
        private int  _row;
        private int _column;


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
            set { _team = value; }
        }
        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }
        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }

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

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column, Top, Bottom, Left, Right);
        }

        public override string ToString()
        {
            return $"Top: {_top}, Bottom: {_bottom}, Left: {_left}, Right: {_right}";
        }
    }
}
