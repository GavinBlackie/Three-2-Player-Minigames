using CheckConnectDot_MAUI_App.DotsBoxes;

namespace CheckConnectDot_MAUI_App;

public partial class DotsBoxesPage : ContentPage
{
	private DotsBoxesGame _dotsBoxesGame;

	public DotsBoxesPage(DotsBoxesGame dotsBoxesGame)
	{
		_dotsBoxesGame = dotsBoxesGame; // Contain the given singleton of a dotsBoxesGame 

        InitializeComponent();
	}
}