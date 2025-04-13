namespace CheckConnectDot_MAUI_App.ConnectFour;

/// <summary>
/// Disk base class represents the disks.
/// </summary>
public abstract class Disk
{
    // Properties
    public int Column { get; protected set; }
    public int Row { get; protected set; }
    public string Color { get; protected set; }
    public string ImageSource { get; }
    public int PlayerNumber { get; }
    
    // Constants for the image files
    protected const string RED_DISK_DIR = "player1_disk.png";
    protected const string BLUE_DISK_DIR = "player2_disk.png";

    /// <summary>
    /// Disk constructor
    /// </summary>
    /// <param name="column">The column of the disk</param>
    /// <param name="row">The row of the disk</param>
    /// <param name="color">The color of the disk</param>
    /// <param name="imageSource">The image sourse of the disk</param>
    /// <param name="playerNumber">The number of the player this disk belongs to</param>
    protected Disk(int column, int row, string color, string imageSource, int playerNumber)
    {
        Column = column;
        Row = row;
        Color = color;
        ImageSource = imageSource;
        PlayerNumber = playerNumber;
    }

    /// <summary>
    /// Virtual method to be implemented by the derived class.
    /// </summary>
    public abstract void OnPlace();
}

/// <summary>
/// PlayerDisk derived class represents the disks being placed by the players.
/// </summary>
public class PlayerDisk : Disk
{
    public PlayerDisk(int column, int row, int playerNumber) : 
            base(column, row, playerNumber == 1 ? "Red" : "Blue",
            playerNumber == 1 ? RED_DISK_DIR : BLUE_DISK_DIR, playerNumber) {}

    /// <summary>
    /// Method to play the place sound when the disk is placed.
    /// </summary>
    public override void OnPlace()
    {
        SoundPlayer.PlayDropSoundAsync();
    }
}