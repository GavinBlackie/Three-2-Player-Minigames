using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers
{
    public class CheckersGame : Game
    {
        public CheckersGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {

        }
    }
}
