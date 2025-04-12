namespace CheckConnectDot_MAUI_App;

public class Connect4Exception : Exception
{
    /// <summary>
    /// Connect4Exception constructor that is derived from base class Exception
    /// </summary>
    /// <param name="message"></param>
    public Connect4Exception(string message) : base(message){}
} 