using UnityEngine;
using UnityEngine.UI;

public static class GameData
{
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

        var progress = Object.FindObjectOfType<Progress>();

        float percentRemaining = BytesRemaining / (float) int.MaxValue;
        progress.Slider.value = percentRemaining;
        progress.Text.text = $"{BytesRemaining} / {int.MaxValue} ({percentRemaining:P})";
    }
}