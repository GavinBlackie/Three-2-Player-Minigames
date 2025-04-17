/*
 * Author: Gavin Blackie
 * File Description:
 *      Defines a single enumeration to be used for the various game states a checkers game might be in
 *      (eg. it could be red's turn, blue's turn, or on the win menu). This enum can also be added onto
 *      to allow for new gamestates (eg. a help menu, or a main menu).
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App.Checkers
{
    /// <summary>
    /// Enumeration representing the various states/menus a checkers 
    /// game may be in.
    /// </summary>
    internal enum CheckersGameState
    {
        RedTurn = 1,
        BlueTurn,
        WinMenu
    }
}
