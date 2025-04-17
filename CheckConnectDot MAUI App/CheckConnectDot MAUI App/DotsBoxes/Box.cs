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
        private string _team;

        public Box(Line top, Line bottom, Line left, Line right, string team)
        {
            _top = top;
            _bottom = bottom;
            _left = left;
            _right = right;
            _team = team;
        }

        private Line Top
        {
            get { return _top; }
        }

        private Line Bottom
        {
            get { return _bottom; }
        }

        private Line Left
        {
            get { return _left; }
        }

        private Line Right
        {
            get { return _right; }
        }
        private String Team
        {
            get { return _team; }
        }
    }

}
