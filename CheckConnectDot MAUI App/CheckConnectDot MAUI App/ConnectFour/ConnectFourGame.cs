using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.ConnectFour
{
    public class ConnectFourGame : Game
    {
        public ConnectFourGame(ref (Player, Player) playersTuple) : base(ref playersTuple)
        {

        }
    }
}
