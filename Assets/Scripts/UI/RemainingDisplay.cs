using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingDisplay : MonoBehaviour
{
	public TextMeshProUGUI text;

	private void Update()
	{
		string remaining = Helpers.FormatBytes(GameData.BytesRemaining);
		text.text = $"{remaining} left to erase";
	}
}
