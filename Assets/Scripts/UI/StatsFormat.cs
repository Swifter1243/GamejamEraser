using System.Collections;
using TMPro;
using UnityEngine;

public class StatsFormat : MonoBehaviour
{
    [TextArea]
    public string formatRateString;
    [TextArea]
    public string formatAutoRateString;
    [TextArea]
    public string formatUserRateString;

    [SerializeField]
    private TMP_Text rateText;
    [SerializeField]
    private TMP_Text autoRateText;
    [SerializeField]
    private TMP_Text userRateText;

    public static int bytesErasedThisSecond = 0;
    public static int bytesAutomatedThisSecond = 0;

    public int Rate
    {
        set { rateText.text = string.Format(formatRateString, Helpers.FormatBytes(value)); }
    }
    public int AutoRate
    {
        set { autoRateText.text = string.Format(formatAutoRateString, Helpers.FormatBytes(value)); }
    }
    public int UserRate
    {
        set { userRateText.text = string.Format(formatUserRateString, Helpers.FormatBytes(value)); }
    }


    void Start()
    {
        StartCoroutine(UpdateStats());
    }

    IEnumerator UpdateStats()
    {
        int lastBytes = GameData.BytesRemaining;

        while (GameData.BytesRemaining > 0)
        {
            Rate = lastBytes - GameData.BytesRemaining;
            UserRate = bytesErasedThisSecond;
            AutoRate = bytesAutomatedThisSecond;

			lastBytes = GameData.BytesRemaining;
            bytesErasedThisSecond = 0;
            bytesAutomatedThisSecond = 0;
            yield return new WaitForSeconds(1f);
        }
    }

}
