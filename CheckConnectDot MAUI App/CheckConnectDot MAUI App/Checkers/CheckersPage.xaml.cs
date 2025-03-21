using CheckConnectDot_MAUI_App.Checkers;

namespace CheckConnectDot_MAUI_App;

public partial class CheckersPage : ContentPage
{
	private CheckersGame _checkersGame;

	public CheckersPage(CheckersGame checkersGame)
	{
		_checkersGame = checkersGame; // Contain the given singleton of a Checkers Game

        InitializeComponent();
	}
}