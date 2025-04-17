using CheckConnectDot_MAUI_App.DotsBoxes;

namespace CheckConnectDot_MAUI_App;

public partial class DotsBoxesPage : ContentPage
{
	private DotsBoxesGame _dotsBoxesGame;

    private Tuple<int, int>? _dotsCoords1;
    private Tuple<int, int>? _dotsCoords2;

    public DotsBoxesPage(DotsBoxesGame dotsBoxesGame)
	{
		_dotsBoxesGame = dotsBoxesGame; // Contain the given singleton of a dotsBoxesGame 
        _dotsCoords1 = null;
        _dotsCoords2 = null;

        InitializeComponent();
	}

	public async void OnDotClicked(object sender, EventArgs e)
	{
        if (sender is View view)
        {
            int row = Grid.GetRow(view);
            int column = Grid.GetColumn(view);

            await DisplayAlert("Button Clicked", $"Box clicked at row {row}, column {column}", "Ok");

        }
    }
}