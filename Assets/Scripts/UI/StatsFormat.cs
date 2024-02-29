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

    public static float timeElapsed = 0;
    private int _lastBytes;
    private UpgradeData[] _upgrades;

    //THIS IS DIRTY :(
    public static int deltaBytes;


    void Start()
    {
        timeElapsed = PlayerPrefs.GetFloat("timeElapsed");
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
        deltaBytes = _lastBytes - GameData.BytesRemaining;
        rateText.text = string.Format(formatRateString, Helpers.FormatBytes(deltaBytes));

        _lastBytes = GameData.BytesRemaining;
    }

	private void Update()
	{
        timeElapsed += Time.deltaTime;
        PlayerPrefs.SetFloat("timeElapsed", timeElapsed);
	}

	public static string GetTimeString()
    {
		TimeSpan value = TimeSpan.FromSeconds(timeElapsed);

		return value.Hours > 0
            ? $"{value.Hours:00}:{value.Minutes:00}:{value.Seconds:00}"
            : $"{value.Minutes:00}:{value.Seconds:00}";
    }

    private void UpdateTimeElapsed()
    {
        timeElapsedText.text = string.Format(formatAutoRateString, GetTimeString());
    }

    private void UpdateUpgradesBought()
    {
        int bought = _upgrades.Sum(up => up.Level);
        int max = _upgrades.Sum(up => up.MaxLevel);

        upgradesPurchasedText.text = string.Format(formatUpgradesBoughtString, bought, max);
    }
}