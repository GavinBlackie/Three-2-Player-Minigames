using CheckConnectDot_MAUI_App.ConnectFour;

namespace CheckConnectDot_MAUI_App;

public partial class ConnectFourPage : ContentPage
{
	private ConnectFourGame _connectFourGame;

	public ConnectFourPage(ConnectFourGame connectFourGame)
	{
		_connectFourGame = connectFourGame; // Contain the given singleton of a connectFourGame

        InitializeComponent();
	}
}