using CheckConnectDot_MAUI_App.DotsBoxes;

namespace CheckConnectDot_MAUI_App;

public partial class DotsBoxesPage : ContentPage
{
	private DotsBoxesGame _dotsBoxesGame;

    private Tuple<int, int> _firstDot;
    private Tuple<int, int> _secondDot;

    public DotsBoxesPage(DotsBoxesGame dotsBoxesGame)
	{
		_dotsBoxesGame = dotsBoxesGame; // Contain the given singleton of a dotsBoxesGame 
        _firstDot = null;
        _secondDot = null;

        InitializeComponent();
	}

    public async void OnDotClicked(object sender, EventArgs e)
    {
        if (sender is View view)
        {
            int row = Grid.GetRow(view);
            int column = Grid.GetColumn(view);

            var currentDot = new Tuple<int, int>(row, column);

            await DisplayAlert("Dot Clicked", $"Box clicked at row {row}, column {column}", "Ok");

            if (_firstDot == null)
            {
                _firstDot = currentDot;
                _lblDot1.Text = $"Dot Selected at {row}, {column}";
                return;
            }

            else if (_firstDot.Equals(currentDot))
            {
                _firstDot = null;
                await DisplayAlert("Dot Clicked", $"You have already selected that dot", "Ok");
                return;
            }
            else
            {
                _secondDot = currentDot;

                _lblDot2.Text = $"Dot Selected at {row}, {column}";

                if (AreDotsAdjacent(_firstDot, _secondDot))
                {
                    await DisplayAlert(" Line Made", $"You have successfully made a line!", "Ok");

                }
                else
                {
                    await DisplayAlert(" Line not made", $"You have didn't make a line!", "Ok");

                    _firstDot = null;
                    _secondDot = null;
                }
            }

        }
    }

        private bool AreDotsAdjacent(Tuple<int, int> dot1, Tuple<int, int> dot2)
    {
        // Checks if they are in the same row and if the columns differ by 2
        if (dot1.Item1 == dot2.Item1 && Math.Abs(dot1.Item2 - dot2.Item2) == 2)
            return true;

        // Checks if they are in the same column and if the rows differ by 2
        if (dot1.Item2 == dot2.Item2 && Math.Abs(dot1.Item1 - dot2.Item1) == 2)
            return true;

        return false;
    }
}