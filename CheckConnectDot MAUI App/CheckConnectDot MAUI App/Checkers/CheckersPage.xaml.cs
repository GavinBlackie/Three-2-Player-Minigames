using CheckConnectDot_MAUI_App.Checkers;
using Microsoft.Maui.Layouts;

namespace CheckConnectDot_MAUI_App;

public partial class CheckersPage : ContentPage
{
    #region Fields

    private CheckersGame _checkersGame;

	private const string BLUE_PIECE_DIR = "bluePiece.png";

	private const string RED_PIECE_DIR = "redPiece.png";

	private const string SELECTED_PIECE_DIR = "selectedPiece.png";

    #endregion

    public CheckersPage(CheckersGame checkersGame)
	{
		_checkersGame = checkersGame; // Contain the given singleton of a Checkers Game

        InitializeComponent();

        AddTiles(); // Add the tiles to the game
        //AddPieces(); // Add the pieces to the game
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

                if ((iTileCol + colShift) % 2 == 0)
                {
                    tileImageBtn.BackgroundColor = Colors.LightGrey;
                }
                else
                {
                    tileImageBtn.BackgroundColor = Colors.Black;
                }
                _gridBoard.Children.Add(tileImageBtn);

                // Credit to Perplexity AI for introducing and giving examples of the SetColumn and SetRow methods
                // https://www.perplexity.ai/search/0749de26-d9c7-4d68-8614-3e80f49c58fa
                Grid.SetColumn(tileImageBtn, iTileCol);
                Grid.SetRow(tileImageBtn, iTileRow);
            }
		}
	}
}