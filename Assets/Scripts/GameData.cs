using System.Linq;
using UnityEngine;
using Upgrades;

public static class GameData
{
    private static ProgressFormat Progress => _progress ? _progress : _progress = Object.FindObjectOfType<ProgressFormat>();
    private static ProgressFormat _progress;

    private static UpgradeData[] AllUpgrades => _allUpgrades ??= Object
        .FindObjectsOfType<UpgradeUI>()
        .Select(ui => ui.Upgrade)
        .ToArray();
    private static UpgradeData[] _allUpgrades;

    private static int TotalSpent => AllUpgrades.Sum(upgrade => upgrade.Spent);

    public static int BytesRemaining
    {
        get => PlayerPrefs.GetInt("bytes_remaining", int.MaxValue);
        private set => PlayerPrefs.SetInt("bytes_remaining", value);
    }

    public static int Currency => int.MaxValue - BytesRemaining - TotalSpent;

    public static void EraseBytes(int amount)
    {
        BytesRemaining -= amount;
        Progress.Progress = BytesRemaining;
    }
}