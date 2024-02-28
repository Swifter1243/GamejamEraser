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
    [TextArea]
    public string formatCurrencyString;

    [SerializeField]
    private TMP_Text rateText;
    [SerializeField]
    private TMP_Text autoRateText;
    [SerializeField]
    private TMP_Text userRateText;
    [SerializeField]
    private TMP_Text currencyText;

    public int Rate
    {
        set { rateText.text = string.Format(formatRateString, value); }
    }
    public int AutoRate
    {
        set { autoRateText.text = string.Format(formatAutoRateString, value); }
    }
    public int UserRate
    {
        set { userRateText.text = string.Format(formatUserRateString, value); }
    }
    public int Currency
    {
        set { currencyText.text = string.Format(formatCurrencyString, value); }
    }


    void Start()
    {
        StartCoroutine(UpdateStats());
    }

    IEnumerator UpdateStats()
    {
        int lastBytes = GameData.BytesRemaining;
        int lastCurrency = GameData.Currency;
        while (GameData.BytesRemaining > 0)
        {
            int deltaBytes = lastBytes - GameData.BytesRemaining;
            Rate = deltaBytes;
            
            //AutoRate = deltaBytes - 
            
            Currency = lastCurrency - GameData.Currency;
            

            lastBytes = GameData.BytesRemaining;
            lastCurrency = GameData.Currency;
            yield return new WaitForSeconds(1f);
        }
    }

}
