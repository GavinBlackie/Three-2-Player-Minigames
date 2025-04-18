
namespace CheckConnectDot_MAUI_App
{
    // Author: Joseph Thomas
    // This is a user-defined exception class that inherits from Exception

    public class DotsBoxesException : Exception
    {

        #region Constructor
        /// <summary>
        /// Constructor for DotsBoxesException that derives from base class Exception
        /// </summary>
        /// <param name="message"></param>
        public DotsBoxesException(string message) : base(message)
        {

        }

        #endregion
    }
}
