using UnityEngine;

public static class GameData
{
    private static Progress Progress => _progress ? _progress : _progress = Object.FindObjectOfType<Progress>();
    private static Progress _progress;

    public static int BytesRemaining
    {
        get => PlayerPrefs.GetInt("bytes_remaining", int.MaxValue);
        private set => PlayerPrefs.SetInt("bytes_remaining", value);
    }

    public static int Currency
    {
        get => PlayerPrefs.GetInt("currency", 0);
        private set => PlayerPrefs.SetInt("currency", value);
    }

    public static void EraseBytes(int amount)
    {
        BytesRemaining -= amount;
        Currency += amount;

        Progress progress = Progress;

        float percentRemaining = BytesRemaining / (float) int.MaxValue;
        progress.Slider.value = percentRemaining;
        progress.Text.text = $"{BytesRemaining} / {int.MaxValue} ({percentRemaining:P})";
    }
}