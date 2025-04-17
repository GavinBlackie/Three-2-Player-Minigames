using Plugin.Maui.Audio; // Import the audio plugin for playing sound in MAUI
// Author: Artem Kotliar
// This is a static class responsible for playing the sound when a disk is placed in the game

public static class SoundPlayer
{
    // Audio player instance
    private static IAudioPlayer? _player;

    // Asynchronous method to play the disk drop sound
    public static async Task PlayDropSoundAsync()
    {
        // Get the current audio manager from the plugin
        IAudioManager audioManager = AudioManager.Current;
        // Open the audio file
        Stream file = await FileSystem.OpenAppPackageFileAsync("disk_place.mp3");
        // Create an audio player for the file
        _player = audioManager.CreatePlayer(file);
        // Play the audio
        _player.Play();
    }
}