using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    public class Box
    {
        private ValueTuple<Line, Line, Line, Line> _pos;
        private string _team;

        public Box(Line Line1, Line Line2, Line Line3, Line Line4, string team)
        {
            _pos = new ValueTuple<Line, Line, Line, Line>(Line1, Line2, Line3, Line4);
            _team = team;
        }
    }
}
