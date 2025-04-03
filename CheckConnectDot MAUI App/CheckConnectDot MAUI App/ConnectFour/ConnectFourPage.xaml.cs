using CheckConnectDot_MAUI_App.ConnectFour;

namespace CheckConnectDot_MAUI_App;

public partial class ConnectFourPage : ContentPage
{
    private ConnectFourGame _connectFourGame;
    private Image[,] _boardImages;

    public ConnectFourPage(ConnectFourGame connectFourGame)
    {
        _connectFourGame = connectFourGame;
        InitializeComponent();
        InitializeBoardReferences();
        SetParams();
        UpdateGameDisplay();
    }

    private void InitializeBoardReferences()
    {
        _boardImages = new Image[7, 6];
        
        // Column 0
        _boardImages[0, 0] = Cell_0_0;
        _boardImages[0, 1] = Cell_0_1;
        _boardImages[0, 2] = Cell_0_2;
        _boardImages[0, 3] = Cell_0_3;
        _boardImages[0, 4] = Cell_0_4;
        _boardImages[0, 5] = Cell_0_5;
        
        // Column 1
        _boardImages[1, 0] = Cell_1_0;
        _boardImages[1, 1] = Cell_1_1;
        _boardImages[1, 2] = Cell_1_2;
        _boardImages[1, 3] = Cell_1_3;
        _boardImages[1, 4] = Cell_1_4;
        _boardImages[1, 5] = Cell_1_5;
        
        // Column 2
        _boardImages[2, 0] = Cell_2_0;
        _boardImages[2, 1] = Cell_2_1;
        _boardImages[2, 2] = Cell_2_2;
        _boardImages[2, 3] = Cell_2_3;
        _boardImages[2, 4] = Cell_2_4;
        _boardImages[2, 5] = Cell_2_5;
        
        // Column 3
        _boardImages[3, 0] = Cell_3_0;
        _boardImages[3, 1] = Cell_3_1;
        _boardImages[3, 2] = Cell_3_2;
        _boardImages[3, 3] = Cell_3_3;
        _boardImages[3, 4] = Cell_3_4;
        _boardImages[3, 5] = Cell_3_5;
        
        // Column 4
        _boardImages[4, 0] = Cell_4_0;
        _boardImages[4, 1] = Cell_4_1;
        _boardImages[4, 2] = Cell_4_2;
        _boardImages[4, 3] = Cell_4_3;
        _boardImages[4, 4] = Cell_4_4;
        _boardImages[4, 5] = Cell_4_5;
        
        // Column 5
        _boardImages[5, 0] = Cell_5_0;
        _boardImages[5, 1] = Cell_5_1;
        _boardImages[5, 2] = Cell_5_2;
        _boardImages[5, 3] = Cell_5_3;
        _boardImages[5, 4] = Cell_5_4;
        _boardImages[5, 5] = Cell_5_5;
        
        // Column 6
        _boardImages[6, 0] = Cell_6_0;
        _boardImages[6, 1] = Cell_6_1;
        _boardImages[6, 2] = Cell_6_2;
        _boardImages[6, 3] = Cell_6_3;
        _boardImages[6, 4] = Cell_6_4;
        _boardImages[6, 5] = Cell_6_5;
    }

    protected void OnCol(object sender, EventArgs e)
    {
        
    }

    private void UpdateCellImage(int col, int row, string imageSource)
    {
        
    }

    private void UpdateTurnDisplay()
    {
        
    }

    private void UpdateDiskCounts(int diskCount1, int diskCount2)
    {
        
    }

    private void UpdateGameDisplay()
    {
        
    }

    protected void SetParams()
    {
        Col1.CommandParameter = 1;
        Col1.Clicked += OnCol;
        
        Col2.CommandParameter = 2;
        Col2.Clicked += OnCol;
        
        Col3.CommandParameter = 3;
        Col3.Clicked += OnCol;
        
        Col4.CommandParameter = 4;
        Col4.Clicked += OnCol;
        
        Col5.CommandParameter = 5;
        Col5.Clicked += OnCol;
        
        Col6.CommandParameter = 6;
        Col6.Clicked += OnCol;
        
        Col7.CommandParameter = 7;
        Col7.Clicked += OnCol;
    }
}