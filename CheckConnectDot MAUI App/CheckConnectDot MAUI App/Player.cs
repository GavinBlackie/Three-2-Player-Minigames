using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckConnectDot_MAUI_App
{
    public class Player
    {
        #region Fields
        private string _name;

        private byte _number;

        private int _numWins;

        private int _numLosses;
        #endregion

        #region Constructors

        public Player(string name, byte number)
        {
            _name = name;
            _number = number;
            _numWins = 0;
            _numLosses = 0;
        }

        #endregion

        #region Properties

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

        public byte Number
        {
            get
            {
                return _number;
            }
        }

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
