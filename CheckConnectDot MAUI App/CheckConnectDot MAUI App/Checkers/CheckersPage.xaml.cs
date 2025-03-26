using CheckConnectDot_MAUI_App.Checkers;
using Microsoft.Maui.Layouts;

namespace CheckConnectDot_MAUI_App;

public partial class CheckersPage : ContentPage
{
    #region Fields

    private CheckersGame _checkersGame;

    private bool _isPieceSelected;

    private ImageButton _lastPieceSelected;

	private const string BLUE_PIECE_DIR = "blue_piece.png";

	private const string RED_PIECE_DIR = "red_piece.png";

	private const string SELECTED_PIECE_DIR = "selected_piece.png";

    #endregion

    public CheckersPage(CheckersGame checkersGame)
	{
		_checkersGame = checkersGame; // Contain the given singleton of a Checkers Game
        _isPieceSelected = false;

        InitializeComponent();

        AddTiles(); // Add the tiles to the game
        AddPieces(); // Add the pieces ontop of the tiles in the game
    }

	private void AddTiles()
	{
		ImageButton imgBtn; // Declare the ImageButton variable that will be used

		// Iterate 8 times for the y (the row)
		for (int iTileRow = 0; iTileRow < 8; iTileRow++)
		{
			int colShift = iTileRow % 2 == 0 ? 1 : 0; // integer representing the shift of the column

            // Iterate 8 times for the x (the columns)
            for (int iTileCol = 0; iTileCol < 8; iTileCol++)
			{
                imgBtn = new ImageButton(); // Create a new ImageButton instance
                imgBtn.WidthRequest = 60.0;
                imgBtn.HeightRequest = 60.0;
                imgBtn.Padding = 10;
                imgBtn.Clicked += OnTile; // Add the OnTile clicked event handler

                // Color each tile in this row accordingly to the colShift integer (makes a checker pattern)
                if ((iTileCol + colShift) % 2 == 0)
                {
                    imgBtn.BackgroundColor = Colors.Black;
                }
                else
                {
                    imgBtn.BackgroundColor = Colors.LightGrey;
                }

                // Add the new presentation-layer tile to the grid (display it!)
                _gridBoard.Children.Add(imgBtn);

                // Credit to Perplexity AI for introducing and giving examples of the SetColumn and SetRow methods
                // https://www.perplexity.ai/search/0749de26-d9c7-4d68-8614-3e80f49c58fa
                Grid.SetColumn(imgBtn, iTileCol);
                Grid.SetRow(imgBtn, iTileRow);

                // Create the new logical Tile instance
                Tile tile = new Tile((iTileCol, iTileRow));

                // Pair the two objects in the CheckersGame dictionary
                _checkersGame.BtnToTile.Add(imgBtn, tile);
            }
		}
	}

    private void AddPieces()
    {
        List<Piece> checkerPieces = _checkersGame.Pieces; // Variable so the property accessor only has to run once

        // Iterate through all of the tiles, if there is a corresponding checker piece at that position, add the corresponding image
        foreach (ImageButton imageButton in _gridBoard.Children)
        {
            // Iterate through the already-made Piece objects in the CheckersGame instance
            for (int iPiece = 0; iPiece < checkerPieces.Count; iPiece++)
            {
                Piece piece = checkerPieces[iPiece];

                // If this piece's position equals the tile's row/col position, add it
                if (piece.Position.xPos == Grid.GetColumn(imageButton) && piece.Position.yPos == Grid.GetRow(imageButton))
                {

                    // Should the piece name be the first one, then make the piece blue, else it will be red
                    if (checkerPieces[iPiece].Team == _checkersGame.TeamNames.team1)
                    {
                        imageButton.Source = BLUE_PIECE_DIR;
                    }
                    else
                    {
                        imageButton.Source = RED_PIECE_DIR;
                    }

                }
            }
        }
    }

    private void OnTile(object sender, EventArgs e)
    {
        // If the object being clicked is an ImageButton tile, then try to 
        if (sender is ImageButton tile)
        {
            // Should the tile have an image inside of it (eg. it has a piece), then try to select it
            if (tile.Source is not null && _isPieceSelected == false)
            {
                tile.Source = SELECTED_PIECE_DIR; // Change the image source to the golden "selected" one
                _isPieceSelected = true;
                _lastPieceSelected = tile; // Save this ImageButton for later use/movements
            }
        }
    }
}