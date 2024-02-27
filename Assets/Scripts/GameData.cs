using UnityEngine;

public static class GameData
{
    public static int BytesRemaining
    {
        get => PlayerPrefs.GetInt("bytes_remaining", int.MaxValue);
        set => PlayerPrefs.SetInt("bytes_remaining", value);
    }

    public static int Currency
    {
        get => PlayerPrefs.GetInt("currency", 0);
        set => PlayerPrefs.SetInt("currency", value);
    }

    public static void EraseBytes(int amount)
    {
        BytesRemaining -= amount;
        Currency += amount;
    }
}