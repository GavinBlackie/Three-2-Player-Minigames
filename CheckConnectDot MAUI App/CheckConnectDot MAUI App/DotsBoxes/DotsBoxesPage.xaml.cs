using CheckConnectDot_MAUI_App.DotsBoxes;
using System;
using System.Linq;


namespace CheckConnectDot_MAUI_App;

public partial class DotsBoxesPage : ContentPage
{
    private DotsBoxesGame _game;
    private Tuple<int, int>? _firstDot;
    private Tuple<int, int>? _secondDot;
    private Dictionary<string, BoxView> _lineViews;

    public DotsBoxesPage(DotsBoxesGame game)
    {
        _game = game;
        _lineViews = new Dictionary<string, BoxView>();
        InitializeComponent();
        CacheLineViews();
    }

    public async void OnDotClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button)
        {
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);
            var currentDot = new Tuple<int, int>(row, col);

            if (_firstDot == null)
            {
                _firstDot = currentDot;
                _lblDot1.Text = $"Dot selected at {_firstDot.Item1 / 2}, {_firstDot.Item2 / 2}";
                return;
            }

            if (_firstDot.Equals(currentDot))
            {
                await DisplayAlert("Invalid", "Dot already selected", "OK");
                return;
            }

            if (_secondDot == null && _firstDot != null)
            {
                _secondDot = currentDot;
                _lblDot2.Text = $"Dot selected at {_firstDot.Item1 / 2}, {_firstDot.Item2 / 2}";
                return;
            }

            else
            {
                await DisplayAlert("Invalid", "Dots have been already selected", "OK");
            }
        }
    }

    public async void OnConnectDots(object sender, EventArgs e)
    {
        if (_firstDot == null || _secondDot == null)
        {
            await DisplayAlert("Error", "Please select two dots first", "OK");
            return;
        }

        if (AreDotsAdjacent(_firstDot, _secondDot))
        {
            var line = _game.CreateLine((byte)_firstDot.Item1 / 2, (byte)_firstDot.Item2 / 2, (byte)_secondDot.Item1 / 2, (byte)_secondDot.Item2 / 2);

            if (_game.IsValidMove(line))
            {
                _game.MakeMove(line);
                DrawLine(_firstDot, _secondDot);

                if (_game.IsGameOver)
                {
                    await DisplayAlert("Game Over", _game.GetWinner(), "OK");
                }
            }
            else
            {
                await DisplayAlert("Invalid", "Line already exists", "OK");
            }
        }
        else
        {
            await DisplayAlert("Invalid", "Dots must be adjacent", "OK");
        }

        ResetSelection();
    }

    private bool AreDotsAdjacent(Tuple<int, int> dot1, Tuple<int, int> dot2)
    {
        return (dot1.Item1 == dot2.Item1 && Math.Abs(dot1.Item2 - dot2.Item2) == 2) ||
               (dot1.Item2 == dot2.Item2 && Math.Abs(dot1.Item1 - dot2.Item1) == 2);
    }

    private void CacheLineViews()
    {
        if (FindView("GameCanvas") is Grid gameCanvas)
        {
            foreach (var child in gameCanvas.Children)
            {
                if (child is BoxView boxView && !string.IsNullOrEmpty(boxView.StyleId))
                {
                    _lineViews[boxView.StyleId] = boxView;
                }
            }
        }
    }

    private void DrawLine(Tuple<int, int> dot1, Tuple<int, int> dot2)
    {
        string lineName = GetLineName(dot1, dot2);
        if (_lineViews.TryGetValue(lineName, out BoxView line))
        {
            line.BackgroundColor = _game.GetPlayerColor();
        }
    }

    private string GetLineName(Tuple<int, int> dot1, Tuple<int, int> dot2)
    {
        if (dot1.Item1 == dot2.Item1) // Horizontal line
        {
            int row = dot1.Item1;
            int col = (dot1.Item2 + dot2.Item2) / 2;
            return $"_line_{row}_{col}";
        }
        else // Vertical line
        {
            int row = (dot1.Item1 + dot2.Item1) / 2;
            int col = dot1.Item2;
            return $"_line_{row}_{col}";
        }
    }

    private View FindView(string name)
    {
        if (this.Content is VerticalStackLayout mainLayout)
        {
            foreach (var child in mainLayout.Children)
            {
                if (child is View view && view.StyleId == name)
                {
                    return view;
                }
            }
        }
        return null;
    }

    private void ResetSelection()
    {
        _firstDot = null;
        _secondDot = null;
        _lblDot1.Text = "";
        _lblDot2.Text = "";
    }
}