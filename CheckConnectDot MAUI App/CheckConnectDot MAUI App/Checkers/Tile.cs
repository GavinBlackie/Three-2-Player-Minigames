/*
 * Author: Gavin Blackie
 * File Description: 
 *      The logical tile's class definition. Meant to define the fields, properties, 
 *      and functionality related to a logical tile instance (not to be confused with
 *      an ImageButton or visible tile)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Layouts;

namespace CheckConnectDot_MAUI_App.Checkers
{
    internal class Tile
    {
        #region Fields

        /// <summary>
        /// A tuple representing this tile's x and y positions
        /// </summary>
        private (int, int) _pos;

        /// <summary>
        /// Nullable piece field, contains a piece when there is a piece instance in this Tile
        /// </summary>
        private Piece? _piece;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor for a new Tile instance, takes in a tuple parameter for the position, and initially sets the
        /// piece container as being blank (tiles initially do not have pieces, pieces are added afterwards).
        /// </summary>
        /// <param name="pos"></param>
        internal Tile((int, int) pos)
        {
            _pos = pos;
            _piece = null; // Initially a tile does not have a piece on it
        }

        #endregion


        #region Properties

        /// <summary>
        /// Read-write property for the piece field of a logical Tile
        /// </summary>
        internal Piece? Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
            }
        }

        /// <summary>
        /// Read-write property for the position of a logical tile
        /// </summary>
        internal (int xPos, int yPos) Position
        {
            get
            {
                return _pos;
            }
            set
            {
                _pos = value;
            }
        }

        #endregion
    }
}
