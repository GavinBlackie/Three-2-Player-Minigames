using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    public class Line
    {
        private ValueTuple<byte, byte> _pos;
        private string _team;

        public Line(byte xCoord, byte yCoord, string team)
        {
            _pos = new ValueTuple<byte, byte>(xCoord, yCoord);
            _team = team;
        }
    }
}
