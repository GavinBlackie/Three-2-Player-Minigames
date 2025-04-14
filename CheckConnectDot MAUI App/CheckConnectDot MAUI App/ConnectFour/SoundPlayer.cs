using Plugin.Maui.Audio;

public static class SoundPlayer
{
    private static IAudioPlayer? _player;

    public static async Task PlayDropSoundAsync()
    {
        IAudioManager audioManager = AudioManager.Current;
        Stream file = await FileSystem.OpenAppPackageFileAsync("disk_place.mp3");
        _player = audioManager.CreatePlayer(file);
        _player.Play();
    }
}