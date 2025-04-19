/*
 * Author: Gavin Blackie
 * File Description:
 *      Defines the Player class, a class responsible for representing a player in some sort of game.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    /// <summary>
    /// Class representing a player/user in a game. Meant to be uniquely identified by a specific player number.
    /// </summary>
    public class Player
    {
        #region Fields

        /// <summary>
        /// String representing this player's name
        /// </summary>
        private string _name;

        /// <summary>
        /// Integral byte value representing this player's unique number
        /// </summary>
        private byte _number;

        /// <summary>
        /// Integer counter for this player's win count
        /// </summary>
        private int _numWins;

        /// <summary>
        /// Integer counter for this player's loss count
        /// </summary>
        private int _numLosses;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for Player objects. Takes in two parameters, a string for
        /// the name, and a byte for this player's number. Sets the win and loss
        /// counters to zero.
        /// </summary>
        /// <param name="name">The new player's name</param>
        /// <param name="number">The new player's unique number</param>
        public Player(string name, byte number)
        {
            _name = name;
            _number = number;
            _numWins = 0;
            _numLosses = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Read-write property for the _name attribute of a Player
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Read-only property for a Player's number
        /// </summary>
        public byte Number
        {
            get
            {
                return _number;
            }
        }

        /// <summary>
        /// Read-write property for a Player's win counter
        /// </summary>
        public int NumWins
        {
            get
            {
                return _numWins;
            }
            set
            {
                _numWins = value;
            }
        }

        /// <summary>
        /// Read-write property for a Players loss counter
        /// </summary>
        public int NumLosses
        {
            get
            {
                return _numLosses;
            }
            set
            {
                _numLosses = value;
            }
        }

        #endregion

    }
}