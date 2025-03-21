using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    /// <summary>
    /// The abstract class that represents some form of game with x number of players.
    /// 
    /// Credit to "Bro Code" for explaining abstract classes in C#:
    /// https://youtu.be/06BrbJm7Sho?list=PLZPZq0r_RZOPNy28FDBys3GVP2LiaIyP_
    /// 
    /// </summary>
    public abstract class Game
    {
        /// <summary>
        /// A tuple collection of the players that exist in this game.
        /// </summary>
        protected ValueTuple<Player> _playerTuple;

        /// <summary>
        /// Constructor for a Game object. Visibility is set to protected only
        /// since abstract classes cannot be instantiated on their own .
        /// (meant to only to be used for base class constructor calls)
        /// </summary>
        /// <param name="playerTuple">A tuple of players for this game</param>
        protected Game(ValueTuple<Player> playerTuple)
        {
            _playerTuple = playerTuple;
        }
    }
}
