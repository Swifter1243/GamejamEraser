using UnityEngine;
using UnityEngine.UI;

public static class GameData
{
    private static int BytesRemaining
    {
        get => PlayerPrefs.GetInt("bytes_remaining", int.MaxValue);
        set => PlayerPrefs.SetInt("bytes_remaining", value);
    }

    private static int Currency
    {
        get => PlayerPrefs.GetInt("currency", 0);
        set => PlayerPrefs.SetInt("currency", value);
    }

    public static void EraseBytes(int amount)
    {
        BytesRemaining -= amount;
        Currency += amount;

        Object.FindObjectOfType<Slider>().value = BytesRemaining / (float) int.MaxValue;
    }
}