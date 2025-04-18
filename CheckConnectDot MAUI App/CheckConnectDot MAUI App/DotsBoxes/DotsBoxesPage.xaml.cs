using CheckConnectDot_MAUI_App.DotsBoxes;
using System;
using System.Linq;
using System.Diagnostics;

namespace CheckConnectDot_MAUI_App;
/* Author: Joseph Thomas */

/// <summary>
/// The XAML cs page for the DotsAndBoxes game
/// </summary>
public partial class DotsBoxesPage : ContentPage
{
    #region Fields
    /// <summary>
    /// A class that handles some of the game logic for the game
    /// </summary>
    private DotsBoxesGame _game;

    /// <summary>
    /// A tuple that holds two ints that represent the first dot
    /// </summary>
    private Tuple<int, int>? _firstDot;

    /// <summary>
    /// A tuple that holds two ints that represent the second dot
    /// </summary>
    private Tuple<int, int>? _secondDot;

    /// <summary>
    /// A Dictionary for the lines between the boxes and their string names
    /// </summary>
    private Dictionary<string, BoxView> _lineViews;

    /// <summary>
    /// A List of created lines stored as strings
    /// </summary>
    private List<string> _createdLines;

    /// <summary>
    /// A Dictionary to map a list of 4 lines to a square on the grid
    /// </summary>
    private Dictionary<Image, List<string>> _boxToLinesMap;

    #endregion


    #region Constructors
    /// <summary>
    /// Constructor a DotsBoxesPage
    /// </summary>
    /// <param name="dotsBoxesGame">Handles most of the game logic</param>

    public DotsBoxesPage(DotsBoxesGame dotsBoxesGame)
    {
        _game = dotsBoxesGame;
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

    #endregion

    #region Methods
    /// <summary>
    /// Handles the click event for dot selection in the Dots and Boxes game.
    /// Manages the selection of two dots that will be connected to form a line.
    /// </summary>
    /// <param name="sender">The ImageButton representing the clicked dot</param>
    /// <param name="e">Event arguments</param>
    public async void OnDotClicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is ImageButton button)
            {
                // Get grid position of clicked dot
                int row = Grid.GetRow(button);
                int col = Grid.GetColumn(button);

                Debug.WriteLine($"{row} {col}");  // Log position for debugging
                var currentDot = new Tuple<int, int>(row, col);

                // First dot selection
                if (_firstDot == null)
                {
                    _firstDot = currentDot;
                    _lblDot1.Text = $"Dot selected at {_firstDot.Item1}, {_firstDot.Item2}";
                    return;
                }

                // Prevent selecting same dot twice
                if (_firstDot.Equals(currentDot))
                {
                    throw new DotsBoxesException("This dot has already been selected");
                }

                // Second dot selection
                if (_secondDot == null && _firstDot != null)
                {
                    _secondDot = currentDot;
                    _lblDot2.Text = $"Dot selected at {_secondDot.Item1}, {_secondDot.Item2}";
                    return;
                }
                else
                {
                    throw new DotsBoxesException("Two dots have been already selected");
                }
            }
        }
        catch (DotsBoxesException ex)
        {
            await DisplayAlert("Invalid", ex.Message, "OK");
        }
    }

    /// <summary>
    /// Connects two selected dots to form a line and handles game logic including:
    /// Line creation and  validation, Box completion detection, Turn management and Game over 
    /// </summary>
    public async void OnConnectDots(object sender, EventArgs e)
    {
        // Validate dot selection
        if (_firstDot == null || _secondDot == null)
        {
            return;  // Early exit if selections incomplete
        }

        try
        {
            if (AreDotsAdjacent(_firstDot, _secondDot))
            {
                // Create line between dots
                var line = _game.CreateLine(
                    _firstDot.Item1,
                    _firstDot.Item2,
                    _secondDot.Item1,
                    _secondDot.Item2
                );

                if (_game.IsValidMove(line))
                {
                    // Preserve current player before any state changes
                    var currentPlayerBeforeMove = _game.GameState;

                    // Add line to game state and draw it
                    _game.Lines.Add(line);
                    DrawLine(_firstDot, _secondDot);

                    // Store created line
                    string lineId = GetLineName(_firstDot, _secondDot);
                    _createdLines.Add(lineId);

                    // Check for box completions
                    bool boxCompleted = false;
                    foreach (var boxEntry in _boxToLinesMap.ToList())
                    {
                        if (boxEntry.Value.All(lineName => _createdLines.Contains(lineName)))
                        {
                            // Complete the box
                            boxEntry.Key.BackgroundColor = _game.GetPlayerColor();
                            _game.MarkBoxAsCompleted(boxEntry.Key);
                            _boxToLinesMap.Remove(boxEntry.Key);
                            boxCompleted = true;
                            await DisplayAlert("Box Completed!", "You get another turn!", "OK");
                        }
                    }

                    // Process the move
                    _game.MakeMove(line);

                    // Only announce turn switch if no boxes were completed
                    if (!boxCompleted)
                    {
                        await DisplayAlert("Turn Switch", $"Now it's {_game.GameState}", "OK");
                    }

                    // Check for game end
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
        }
        catch (DotsBoxesException ex)
        {
            await DisplayAlert("Invalid", ex.Message, "OK");
        }

        // Clear selections regardless of outcome
        ResetSelection();
    }

    /// <summary>
    /// Resets the dots and text fields
    /// </summary>
    /// <param name="sender">The reset button</param>
    /// <param name="e">EventArgs</param>
    public void OnResetDots(object sender, EventArgs e)
    {
        ResetSelection();
    }

    /// <summary>
    /// Checks to see if two dots are adjacent or not
    /// </summary>
    /// <param name="dot1"></param>
    /// <param name="dot2"></param>
    /// <returns></returns>
    private bool AreDotsAdjacent(Tuple<int, int> dot1, Tuple<int, int> dot2)
    {
        return (dot1.Item1 == dot2.Item1 && Math.Abs(dot1.Item2 - dot2.Item2) == 2) ||
               (dot1.Item2 == dot2.Item2 && Math.Abs(dot1.Item1 - dot2.Item1) == 2);
    }

    /// <summary>
    /// Gets the box view name of a provided line
    /// </summary>
    /// <param name="dot1">The first point of the line</param>
    /// <param name="dot2">The second point of the line</param>
    /// <returns>A string with a box view equivalent name</returns>
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

    /// <summary>
    /// Searches through the page's visual tree to find a view with the specified StyleId.
    /// </summary>
    /// <param name="name">The StyleId of the view to find</param>
    /// <returns>The found View object, or null if not found</returns>
    private View FindView(string name)
    {
        // Check if the page's main content is a VerticalStackLayout
        if (this.Content is VerticalStackLayout mainLayout)
        {
            // Iterate through all child views in the layout
            foreach (var child in mainLayout.Children)
            {
                // Check if the child is a View and has a matching StyleId
                if (child is View view && view.StyleId == name)
                {
                    return view; // Return the matching view
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Caches all BoxView line elements from the GameCanvas grid into a dictionary for efficient lookup
    /// </summary>
    private void CacheLineViews()
    {
        // Find the GameCanvas grid using FindView method
        if (FindView("GameCanvas") is Grid gameCanvas)
        {
            // Iterate through all children of the GameCanvas grid
            foreach (var child in gameCanvas.Children)
            {
                // Look for BoxView elements that have a non-empty StyleId
                if (child is BoxView boxView && !string.IsNullOrEmpty(boxView.StyleId))
                {
                    // Store the BoxView in a dictionary using its StyleId as the key
                    _lineViews[boxView.StyleId] = boxView;
                }
            }
        }
    }

    /// <summary>
    /// Draws a line between two points with a players color
    /// </summary>
    /// <param name="dot1">The first point, where start drawing</param>
    /// <param name="dot2">The second point, where to stop drawing</param>
    private void DrawLine(Tuple<int, int> dot1, Tuple<int, int> dot2)
    {
        // Gets the box view name of the line
        string lineName = GetLineName(dot1, dot2);

        // If a value is found, paints the line the color of the player who captured it
        if (_lineViews.TryGetValue(lineName, out BoxView line))
        {
            line.BackgroundColor = _game.GetPlayerColor();
        }
    }

    /// <summary>
    /// Resets the selection of dots and text
    /// </summary>
    private void ResetSelection()
    {
        _firstDot = null;
        _secondDot = null;
        _lblDot1.Text = "";
        _lblDot2.Text = "";
    }

    /// <summary>
    /// Updates the captured square with the player who captured it color
    /// </summary>
    /// <param name="box">The box to be updated</param>
    private void UpdateBoxAppearance(Box box)
    {
        // Convert box coordinates to the correct Grid position
        int gridRow = box.Row * 2 + 1;
        int gridColumn = box.Column * 2 + 1;

        // Chooses a color based on which players turn it is
        Color color;

        if (box.Team == DotsandBoxesGameState.BluePlayerTurn) 
        {
            color = Colors.Blue;
        }
        else if (box.Team == DotsandBoxesGameState.RedPlayerTurn)
        {
            color = Colors.Red;
        }
        else
        {
            color = Colors.Gray;
        }

        // Create a colored square for the captured square
        BoxView boxFill = new BoxView
        {
            Color = color,
            CornerRadius = 2,
            Opacity = 0.3,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill
        };
    }
    
    /// <summary>
    /// Private method that generatees the boxes based on their position in the grid, and add them to a list
    /// </summary>
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

    #endregion
}