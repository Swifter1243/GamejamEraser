using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Upgrades;

public class StatsFormat : MonoBehaviour
{
    [TextArea]
    public string formatRateString;

    [TextArea]
    public string formatAutoRateString;

    [TextArea]
    public string formatUpgradesBoughtString;

    [SerializeField]
    private TMP_Text rateText;

    [SerializeField]
    private TMP_Text timeElapsedText;

    [SerializeField]
    private TMP_Text upgradesPurchasedText;

    private float _startTime;
    private int _lastBytes;
    private UpgradeData[] _upgrades;

    void Start()
    {
        _startTime = Time.time;
        _lastBytes = GameData.BytesRemaining;
        _upgrades = FindObjectsOfType<UpgradeUI>()
            .Select(up => up.Upgrade)
            .ToArray();

        StartCoroutine(UpdateStats());
    }

    IEnumerator UpdateStats()
    {
        while (GameData.BytesRemaining > 0)
        {
            UpdateRate();
            UpdateTimeElapsed();
            UpdateUpgradesBought();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateRate()
    {
        int rate = _lastBytes - GameData.BytesRemaining;
        rateText.text = string.Format(formatRateString, Helpers.FormatBytes(rate));

        _lastBytes = GameData.BytesRemaining;
    }

    private void UpdateTimeElapsed()
    {
        TimeSpan value = TimeSpan.FromSeconds(Time.time - _startTime);

        string time = value.Hours > 0
            ? $"{value.Hours:00}:{value.Minutes:00}:{value.Seconds:00}"
            : $"{value.Minutes:00}:{value.Seconds:00}";

        timeElapsedText.text = string.Format(formatAutoRateString, time);
    }

    private void UpdateUpgradesBought()
    {
        int bought = _upgrades.Sum(up => up.Level);
        int max = _upgrades.Sum(up => up.MaxLevel);

        upgradesPurchasedText.text = string.Format(formatUpgradesBoughtString, bought, max);
    }
}