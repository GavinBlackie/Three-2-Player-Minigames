using CheckConnectDot_MAUI_App.Checkers;
using CheckConnectDot_MAUI_App.ConnectFour;
using CheckConnectDot_MAUI_App.DotsBoxes;
using Microsoft.Extensions.Logging;

namespace CheckConnectDot_MAUI_App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		
#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Create two new players and place them in a tuple
		Player player1 = new Player("Player 1", 1);
		Player player2 = new Player("Player 2", 2);
		(Player, Player) playerTuple = (player1, player2);

		// Create all the minigames with references to the initial player objects
		CheckersGame checkersGame = new CheckersGame(ref playerTuple);
		ConnectFourGame connectFourGame = new ConnectFourGame(ref playerTuple);
		DotsBoxesGame dotsBoxesGame = new DotsBoxesGame(ref playerTuple);

		// Add the games as singleton objects to the MAUI program
		builder.Services.AddSingleton(checkersGame);
		builder.Services.AddSingleton(connectFourGame);
        builder.Services.AddSingleton(dotsBoxesGame);

        return builder.Build();
	}
}
