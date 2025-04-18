namespace CheckConnectDot_MAUI_App;
using System.Text.Json;
// Author: Artem Kotliar
// This is a handler for saving and loading data from a json file.
public class JSONHandler
{
    // Name of the player data file
    private const string FILE_NAME = "PlayerData.json";
    
    /// <summary>
    /// Method to save the game data
    /// </summary>
    /// <param name="playerTuple">The two players</param>
    /// <exception cref="Connect4Exception">Thrown if an error occurs while writing the data to the file</exception>
    public static void SaveGameData((Player, Player) playerTuple)
    {
        PlayerData playerData = new PlayerData
        {
            Player1Name = playerTuple.Item1.Name,
            Player1Wins = playerTuple.Item1.NumWins,
            Player2Name = playerTuple.Item2.Name,
            Player2Wins = playerTuple.Item2.NumWins,
        };

        string filePath = Path.Combine(FileSystem.AppDataDirectory, FILE_NAME);

        try
        {
            JsonSerializerOptions options = new() { WriteIndented = true };
            string json = JsonSerializer.Serialize(playerData, options);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            //throw new Connect4Exception($"Failed to save game data: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads the game data
    /// </summary>
    /// <returns>If the file doesn't exist or fails to load then returns empty data or throws an exception.</returns>
    /// <exception cref="Connect4Exception">Thrown if an error occurs while reading or deserializing the file</exception>
    public static PlayerData LoadGameData()
    {
        string filePath = Path.Combine(FileSystem.AppDataDirectory, FILE_NAME);

        if (!File.Exists(filePath))
        {
            return new PlayerData(); // Return empty data if file doesn't exist
        }

        try
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<PlayerData>(json) ?? new PlayerData();
        }
        catch (Exception ex)
        {
            throw new DotsBoxesException($"Failed to load game data: {ex.Message}");
        }
    }
}