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
    private List<string> _createdLines;

    private Dictionary<Image, List<string>> _boxToLinesMap;

    public DotsBoxesPage(DotsBoxesGame game)
    {
        _game = game;
        _lineViews = new Dictionary<string, BoxView>();
        _createdLines = new List<string>();
        _boxToLinesMap = new Dictionary<Image, List<string>>();

        InitializeComponent();


        _boxToLinesMap.Add(_img1_1, new List<string> { "_line_0_1", "_line_2_1", "_line_1_0", "_line_1_2" });
        _boxToLinesMap.Add(_img1_3, new List<string> { "_line_0_3", "_line_2_3", "_line_1_2", "_line_1_4" });
        _boxToLinesMap.Add(_img1_5, new List<string> { "_line_0_5", "_line_2_5", "_line_1_4", "_line_1_6" });
        _boxToLinesMap.Add(_img1_7, new List<string> { "_line_0_7", "_line_2_7", "_line_1_6", "_line_1_8" });

        _boxToLinesMap.Add(_img3_1, new List<string> { "_line_2_1", "_line_4_1", "_line_3_0", "_line_3_2" });
        _boxToLinesMap.Add(_img3_3, new List<string> { "_line_2_3", "_line_4_3", "_line_3_2", "_line_3_4" });
        _boxToLinesMap.Add(_img3_5, new List<string> { "_line_2_5", "_line_4_5", "_line_3_4", "_line_3_6" });
        _boxToLinesMap.Add(_img3_7, new List<string> { "_line_2_7", "_line_4_7", "_line_3_6", "_line_3_8" });

        _boxToLinesMap.Add(_img5_1, new List<string> { "_line_4_1", "_line_6_1", "_line_5_0", "_line_5_2" });
        _boxToLinesMap.Add(_img5_3, new List<string> { "_line_4_3", "_line_6_3", "_line_5_2", "_line_5_4" });
        _boxToLinesMap.Add(_img5_5, new List<string> { "_line_4_5", "_line_6_5", "_line_5_4", "_line_5_6" });
        _boxToLinesMap.Add(_img5_7, new List<string> { "_line_4_7", "_line_6_7", "_line_5_6", "_line_5_8" });

        _boxToLinesMap.Add(_img7_1, new List<string> { "_line_6_1", "_line_8_1", "_line_7_0", "_line_7_2" });
        _boxToLinesMap.Add(_img7_3, new List<string> { "_line_6_3", "_line_8_3", "_line_7_2", "_line_7_4" });
        _boxToLinesMap.Add(_img7_5, new List<string> { "_line_6_5", "_line_8_5", "_line_7_4", "_line_7_6" });
        _boxToLinesMap.Add(_img7_7, new List<string> { "_line_6_7", "_line_8_7", "_line_7_6", "_line_7_8" });

        CacheLineViews();
        BoxGeneration();
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
        if (_firstDot == null || _secondDot == null) return;

        if (AreDotsAdjacent(_firstDot, _secondDot))
        {
            var line = _game.CreateLine(
                _firstDot.Item1,
                _firstDot.Item2,
                _secondDot.Item1,
                _secondDot.Item2
            );

            if (_game.IsValidMove(line))
            {
                // Store current player before making the move
                var currentPlayerBeforeMove = _game.GameState;

                _game.Lines.Add(line);
                DrawLine(_firstDot, _secondDot);

                string lineId = GetLineName(_firstDot, _secondDot);
                _createdLines.Add(lineId);

                // Check for completed boxes
                bool boxCompleted = false;
                foreach (var boxEntry in _boxToLinesMap.ToList())
                {
                    if (boxEntry.Value.All(lineName => _createdLines.Contains(lineName)))
                    {
                        boxEntry.Key.BackgroundColor = _game.GetPlayerColor();
                        _game.MarkBoxAsCompleted(boxEntry.Key);
                        _boxToLinesMap.Remove(boxEntry.Key);
                        boxCompleted = true;
                        await DisplayAlert("Box Completed!", "You get another turn!", "OK");
                    }
                }

                _game.MakeMove(line);

                if (!boxCompleted)
                {
                    await DisplayAlert("Turn Switch", $"Now it's {_game.GameState}", "OK");
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

    public void OnResetDots(object sender, EventArgs e)
    {
        ResetSelection();
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

    private void UpdateBoxAppearance(Box box)
    {
        // Convert box coordinates to the correct Grid position (center of the box)
        int gridRow = box.Row * 2 + 1;
        int gridColumn = box.Column * 2 + 1;

        // Create a colored square (or find existing BoxView if you're updating)
        var color = box.Team == DotsandBoxesGameState.BluePlayerTurn ? Colors.Blue :
                    box.Team == DotsandBoxesGameState.RedPlayerTurn ? Colors.Red :
                    Colors.Gray;

        var boxFill = new BoxView
        {
            Color = color,
            CornerRadius = 2,
            Opacity = 0.3,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill
        };
    }


    private void DrawLine(Tuple<int, int> dot1, Tuple<int, int> dot2)
    {
        string lineName = GetLineName(dot1, dot2);
        if (_lineViews.TryGetValue(lineName, out BoxView line))
        {
            line.BackgroundColor = _game.GetPlayerColor();
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

    private void BoxGeneration()
    {
        for (int y = 0; y < 4; y++) // Top to bottom (0 to 3)
        {
            for (int x = 0; x < 4; x++) // Left to right (0 to 3)
            {
                Box box = new Box(
                    new Line(x * 2, y * 2, x * 2, (y + 1) * 2, DotsandBoxesGameState.None),           // Left
                    new Line((x + 1) * 2, y * 2, (x + 1) * 2, (y + 1) * 2, DotsandBoxesGameState.None), // Right
                    new Line(x * 2, y * 2, (x + 1) * 2, y * 2, DotsandBoxesGameState.None),           // Top
                    new Line(x * 2, (y + 1) * 2, (x + 1) * 2, (y + 1) * 2, DotsandBoxesGameState.None), // Bottom
                    DotsandBoxesGameState.None
                );
                Debug.WriteLine($"Testing box at grid position: ({box.Row * 2 + 1},{box.Column * 2 + 1})");
                _game.Boxes.Add(box);
                UpdateBoxAppearance(box);
            }
        }
    }

}