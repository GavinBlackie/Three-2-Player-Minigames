using CheckConnectDot_MAUI_App.Checkers;
using Microsoft.Maui.Layouts;

namespace CheckConnectDot_MAUI_App;

public partial class CheckersPage : ContentPage
{
    #region Fields

    private CheckersGame _checkersGame;

	private const string BLUE_PIECE_DIR = "blue_piece.png";

	private const string RED_PIECE_DIR = "red_piece.png";

	private const string SELECTED_PIECE_DIR = "selectedPiece.png";

    #endregion

    public CheckersPage(CheckersGame checkersGame)
	{
		_checkersGame = checkersGame; // Contain the given singleton of a Checkers Game

        InitializeComponent();

        AddTiles(); // Add the tiles to the game
        AddPieces(); // Add the pieces to the game
    }

	private void AddTiles()
	{
		ImageButton tileImageBtn; // Declare the ImageButton variable that will be used

		// Iterate 8 times for the y (the row)
		for (int iTileRow = 0; iTileRow < 8; iTileRow++)
		{
            // integer representing the shift of the column
			int colShift = iTileRow % 2 == 0 ? 1 : 0;

			// Iterate 8 times for the x (the columns)
			for (int iTileCol = 0; iTileCol < 8; iTileCol++)
			{
                tileImageBtn = new ImageButton(); // Create a new ImageButton instance
                tileImageBtn.WidthRequest = 60.0;
                tileImageBtn.HeightRequest = 60.0;

                // Color each tile in this row accordingly to the colShift integer (makes a checker pattern)
                if ((iTileCol + colShift) % 2 == 0)
                {
                    tileImageBtn.BackgroundColor = Colors.Black;
                }
                else
                {
                    tileImageBtn.BackgroundColor = Colors.LightGrey;
                }
                _gridBoard.Children.Add(tileImageBtn);

                // Credit to Perplexity AI for introducing and giving examples of the SetColumn and SetRow methods
                // https://www.perplexity.ai/search/0749de26-d9c7-4d68-8614-3e80f49c58fa
                Grid.SetColumn(tileImageBtn, iTileCol);
                Grid.SetRow(tileImageBtn, iTileRow);
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
                if (piece.Position.Item1 == Grid.GetColumn(imageButton) && piece.Position.Item2 == Grid.GetRow(imageButton))
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
}