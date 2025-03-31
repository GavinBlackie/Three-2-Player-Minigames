using System.Diagnostics;
using System.IO.Pipelines;
using CheckConnectDot_MAUI_App.Checkers;
using CheckConnectDot_MAUI_App.Checkers.Exceptions;
using Microsoft.Maui.Layouts;

namespace CheckConnectDot_MAUI_App;

public partial class CheckersPage : ContentPage
{
    #region Fields

    /// <summary>
    /// The CheckerGame instance for this CheckersPage
    /// </summary>
    private CheckersGame _checkersGame;

    /// <summary>
    /// True or false statement on if a piece is currently selected
    /// </summary>
    private bool _isPieceSelected;

    /// <summary>
    /// ImageButton field that represents the last clicked button
    /// </summary>
    private ImageButton _lastImageButtonSelected;

    /// <summary>
    /// Directory string of the blue piece image
    /// </summary>
	private const string BLUE_PIECE_DIR = "blue_piece.png";

    /// <summary>
    /// Directory string of the red piece image
    /// </summary>
	private const string RED_PIECE_DIR = "red_piece.png";

    /// <summary>
    /// Directory string of the selected piece image
    /// </summary>
	private const string SELECTED_PIECE_DIR = "selected_piece.png";

    #endregion

    /// <summary>
    /// Constructor for a new CheckersPage instance. Accepts a single checkersGame parameter
    /// (preferably a singleton instance).
    /// </summary>
    /// <param name="checkersGame"></param>
    public CheckersPage(CheckersGame checkersGame)
	{
		_checkersGame = checkersGame; // Contain the given singleton of a Checkers Game
        _isPieceSelected = false;
        _lastImageButtonSelected = new ImageButton(); // Place a placeholder instance for the last piece selected

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

                // Make sure that the tile references a piece if required
                for (int iPiece = 0; iPiece < _checkersGame.Pieces.Count; iPiece++)
                {
                    Piece piece = _checkersGame.Pieces[iPiece];
                    if (piece.Position == tile.Position)
                    {
                        tile.Piece = piece;
                    }
                }

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
                    DefaultPieceSource(imageButton, piece); // Use the default image source for that piece
                }
            }
        }
    }

    private void OnTile(object sender, EventArgs e)
    {
        // If the object being clicked is an ImageButton tile, then try to act accordingly
        if (sender is ImageButton imageButton)
        {
            // Attempt to select a piece
            if (AttemptPieceSelect(imageButton))
            {
                return; // Preemtively end call should a piece have been selected
            }

            // Try to move a piece, if you could not, try to capture a piece, if you could not - deselect
            try
            {
                AttemptMove(imageButton);
            }
            // Catch cases where a move is determined invalid between the two tiles
            catch (InvalidPieceMove)
            {
                try
                {
                    AttemptCapture(imageButton);
                }
                catch (InvalidCaptureMove ex)
                {
                    if (ex.Piece is Piece piece)
                    {
                        DefaultPieceSource(_lastImageButtonSelected, piece);
                        _isPieceSelected = false; // Declare that no piece is selected
                    }
                }
            }
        }
    }

    private bool AttemptPieceSelect(ImageButton imageButton)
    {
        // Should the imageButton (the "visible" tile) have an image inside of it (eg. its image source is not empty), then try to select it
        if (imageButton.Source is not null && _checkersGame.BtnToTile[imageButton].Piece is not null 
            && _isPieceSelected == false)
        {
            if ( (int)_checkersGame.BtnToTile[imageButton].Piece.Team == (int)_checkersGame.GameState)
            {
                imageButton.Source = SELECTED_PIECE_DIR; // Change the image source to the golden "selected" one
                _isPieceSelected = true;
                _lastImageButtonSelected = imageButton; // Save this ImageButton for later use/movements
                return true;
            }
        }
        return false;
    }

    private bool AttemptMove(ImageButton imageButton)
    {
        // Should a piece already be selected, attempt to move
        if (_isPieceSelected)
        {
            // First, logically move the pieces
            Piece pieceMoved = _checkersGame.MovePiece(ref _lastImageButtonSelected, ref imageButton);

            // Then, move the piece images on the board
            imageButton.Source = _lastImageButtonSelected.Source;
            _lastImageButtonSelected.Source = null;

            // Default the piece's display image after moving
            DefaultPieceSource(imageButton, pieceMoved);

            _isPieceSelected = false; // Declare that no piece is selected

            return true;
        }
        return false;
    }

    private bool AttemptCapture(ImageButton imageButton)
    {
        // Should a piece already be selected, attempt to capture
        if (_isPieceSelected)
        {
            // First, logically capture the pieces
            Piece pieceMoved = _checkersGame.CapturePiece(ref _lastImageButtonSelected, ref imageButton);

            // Then, move the piece images on the board
            imageButton.Source = _lastImageButtonSelected.Source;
            _lastImageButtonSelected.Source = null;

            // Default the piece's display image after moving
            DefaultPieceSource(imageButton, pieceMoved);

            _isPieceSelected = false; // Declare that no piece is selected

            return true;
        }
        return false;
    }

    private void DefaultPieceSource(ImageButton imageButton, Piece piece)
    {
        switch (piece.Team)
        {
            case Team.Blue:
                imageButton.Source = BLUE_PIECE_DIR;
                break;
            case Team.Red:
                imageButton.Source = RED_PIECE_DIR;
                break;
            default:
                Debug.Assert(false, "Unexpected team name, defaulting to the selected piece image");
                imageButton.Source = SELECTED_PIECE_DIR;
                break;
        }
    }
}