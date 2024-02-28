using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
	public TextMeshProUGUI text;

	private void Update()
	{
		string currency = Helpers.FormatBytes(GameData.Currency);
		text.text = $"Free: {currency}";
	}
}
