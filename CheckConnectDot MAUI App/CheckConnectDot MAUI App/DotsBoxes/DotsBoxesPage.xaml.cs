using CheckConnectDot_MAUI_App.DotsBoxes;
using System;
using System.Linq;
using System.Diagnostics;



namespace CheckConnectDot_MAUI_App;

public partial class DotsBoxesPage : ContentPage
{
    private DotsBoxesGame _game;
    private Tuple<int, int>? _firstDot;
    private Tuple<int, int>? _secondDot;
    private Dictionary<string, BoxView> _lineViews;
    private Dictionary<string, Image> _boxViews;

    private List<string> _boxImageNames;


    public DotsBoxesPage(DotsBoxesGame game)
    {
        _game = game;
        _lineViews = new Dictionary<string, BoxView>();
        _boxViews = new Dictionary<string, Image>();
        _boxImageNames = new List<string> {
            "_img1_1", "_img1_3", "_img1_5", "_img1_7",
            "_img3_1", "_img3_3", "_img3_5", "_img3_7",
            "_img5_1", "_img5_3", "_img5_5", "_img5_7",
            "_img7_1", "_img7_3", "_img7_5", "_img7_7"};

        InitializeComponent();
        CacheLineViews();
        CacheBoxViews();
        TestBoxFinding();
    }

    public async void OnDotClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button)
        {
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            Debug.WriteLine($"{row} {col}");
            var currentDot = new Tuple<int, int>(row, col);

            if (_firstDot == null)
            {
                _firstDot = currentDot;
                _lblDot1.Text = $"Dot selected at {_firstDot.Item1}, {_firstDot.Item2}";
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
                _lblDot2.Text = $"Dot selected at {_secondDot.Item1}, {_secondDot.Item2}";
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
            var line = _game.CreateLine(
                _firstDot.Item1 ,
                _firstDot.Item2,
                _secondDot.Item1,
                _secondDot.Item2
            );

            if (_game.IsValidMove(line))
            {
                _game.Lines.Add(line);
                _game.MakeMove(line);
                DrawLine(_firstDot, _secondDot);

                var completedBoxes = _game.CheckForCompletedBoxes(line);
                foreach (var box in completedBoxes)
                {
                    Debug.WriteLine(box);
                    await DisplayAlert("Box made", $"Box row col: {box.GridRow}, {box.GridCol}", "OK");
                    UpdateBoxAppearance(box);
                }

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

    private async void CacheBoxViews()
    {
        _boxViews = new Dictionary<string, Image>();

        if (this.FindByName<Grid>("GameCanvas") is Grid gameCanvas)
        {
            foreach (var child in gameCanvas.Children)
            {
                if (child is Image image && image.StyleId?.StartsWith("_img") == true)
                {
                    _boxViews[image.StyleId] = image;
                    await DisplayAlert("Box", $"Cached box: {image.StyleId} at ({Grid.GetRow(image)},{Grid.GetColumn(image)})", "Ok");
                }
            }
        }
    }


    private void UpdateBoxAppearance(Box box)
    {
        string boxName = $"_img{box.GridRow}_{box.GridCol}";

        Debug.WriteLine($"_img{box.GridRow}_{box.GridCol}");

        if (_boxImageNames.Contains(boxName))
        {
            Color color; 
            
            if(box.Team == DotsandBoxesGameState.BluePlayerTurn)
            {
                color = Colors.Blue;
            }
            else if(box.Team == DotsandBoxesGameState.RedPlayerTurn)
            {
                color = Colors.Red;
            }
            else
            {
                color = Colors.Gray;
            }

            var boxImage = this.FindByName<Image>(boxName);

            if (boxImage != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    boxImage.BackgroundColor = color;
                });
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

    private void TestBoxFinding()
    {
        Box testBox = new Box(
            new Line(1, 0, 1, 2, DotsandBoxesGameState.BluePlayerTurn), // Left
            new Line(2, 0, 2, 2, DotsandBoxesGameState.BluePlayerTurn), // Right
            new Line(1, 0, 2, 0, DotsandBoxesGameState.BluePlayerTurn), // Top
            new Line(1, 2, 2, 2, DotsandBoxesGameState.BluePlayerTurn), // Bottom
            DotsandBoxesGameState.BluePlayerTurn);

        Debug.WriteLine($"Testing box at grid position: ({testBox.GridRow},{testBox.GridCol})");

        UpdateBoxAppearance(testBox);  // Should update _img1_1
    }

}