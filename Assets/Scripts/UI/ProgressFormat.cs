using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ProgressFormat : MonoBehaviour
{
	[Header("TMP_Text is updated when progress is set, format string requires a string, and two integers. (Progress, nominator, demoninator")]
	[TextArea]
	public string formatString;

	const int PROGRESS_CHAR_LEN = 48;

	const char PROGRESS_CHAR_EPTY = ' ';
	const char PROGRESS_CHAR_FULL = '█';

    readonly char[] progressChars = { '░', '▒', '▓'}; //See: Fence post problem


    public int Progress
	{
		set 
		{
			//Get the fractional value 
			float frac = (float)value / int.MaxValue;
            float segmentFrac = frac * PROGRESS_CHAR_LEN;
			char[] chars = new char[PROGRESS_CHAR_LEN];
			for (int i = 0; i < PROGRESS_CHAR_LEN; i++)
			{
				float curFrac = segmentFrac - i;

                if (curFrac >= 1) //Solid
				{
					chars[i] = PROGRESS_CHAR_FULL;
				}
				else if (curFrac > 0) //In-progress
				{
					chars[i] = progressChars[Mathf.FloorToInt(curFrac * progressChars.Length)];
				}
				else //Blank
				{
					chars[i] = PROGRESS_CHAR_EPTY;
				}
			}

			text.text = string.Format(formatString, new string(chars), frac);
			//text.text = string.Format(formatString, chars, value, int.MaxValue);
		}
	}

	[SerializeField]
	private int myVar;
	private TMP_Text text;

	void Start()
	{
		text = GetComponent<TMP_Text>();   
	}
}
