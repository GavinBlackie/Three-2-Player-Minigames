using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    public class Line
    {
        private byte _x1;
        private byte _y1;
        private byte _x2;
        private byte _y2;

        private DotsandBoxesGameState _team;
        private bool _isVertical;
        private bool _isHorizontal;

        public Line(byte x1, byte y1, byte x2, byte y2, DotsandBoxesGameState team)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
            _team = team;

            _isVertical = IsLineVertical();
            _isHorizontal = IsLineHorizontal();
        }

        public byte X1
        {
            get { return _x1; }
        }

        public byte Y1
        {
            get { return _y1; }
        }

        public byte X2
        {
            get { return _x2; }
        }

        public byte Y2
        {
            get { return _y2; }
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
    }
}
