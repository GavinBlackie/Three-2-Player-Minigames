using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.DotsBoxes
{
    public class DotsBoxesGame : Game
    {
        public DotsBoxesGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {

        }
    }
}
