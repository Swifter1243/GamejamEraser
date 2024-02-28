using TMPro;
using UnityEngine;
using System;
using System.Collections;

public class StartupFormat : MonoBehaviour
{
	[Header("String format for ")]
	[TextArea(1, int.MaxValue)]
	[SerializeField]
	private string format;

	[SerializeField] private TMP_Text text;

	private string[] lines;

	const float STARTUP_WAIT = 5f;
	const float STARTUP_WAIT2 = 1f;
	const float STARTUP_CHAR_MIN_DELAY = 0.00f;
	const float STARTUP_CHAR_MAX_DELAY = 0.04f;
	const float STARTUP_CHAR_THRESHOLD = 0.038f;


	const float STARTUP_MIN_DELAY = 0.02f;
	const float STARTUP_MAX_DELAY = 0.08f;
	
	void Start()
	{
		DateTime time = DateTime.Now;
		lines = string.Format(format, 
			time.DayOfWeek.ToString().Substring(0,3),
			2, //febuary
			20+(int)time.DayOfWeek, //20th + day of week
			1994, //year 1994
			time.Hour,
			time.Minute,
			time.Second,
			time.Millisecond/10
			).Split('\n');

	}

	IEnumerator NextLine(Action callback)
	{
		yield return new WaitForSeconds(STARTUP_WAIT);

		for (int i = 0; i < lines.Length; i++)
		{
			text.text += '\n';
			for(int j = 0; j < lines[i].Length; j++)
			{
				text.text += lines[i][j];

				float range = UnityEngine.Random.Range(
						STARTUP_CHAR_MIN_DELAY,
						STARTUP_CHAR_MAX_DELAY);
				
				//Sometimes put more than 1 character
				if (range > STARTUP_CHAR_THRESHOLD) yield return new WaitForSeconds(range);
			}

			//Wait during line
			yield return new WaitForSeconds(
				UnityEngine.Random.Range(
					STARTUP_MIN_DELAY, 
					STARTUP_MAX_DELAY));
		}

        yield return new WaitForSeconds(STARTUP_WAIT2);
        callback();
	}
	public void Startup(Action callback) => StartCoroutine(NextLine(callback));
}
