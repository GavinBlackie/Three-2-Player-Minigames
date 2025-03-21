using CheckConnectDot_MAUI_App.Checkers;

namespace CheckConnectDot_MAUI_App;

public partial class CheckersPage : ContentPage
{
    #region Fields

    private CheckersGame _checkersGame;

	private const string BLUE_PIECE_DIR = "bluePiece.png";

	private const string RED_PIECE_DIR = "redPiece.png";

	private const string SELECTED_PIECE_DIR = "selectedPiece.png";

    #endregion

    public CheckersPage(CheckersGame checkersGame)
	{
		_checkersGame = checkersGame; // Contain the given singleton of a Checkers Game

        InitializeComponent();
	}
}