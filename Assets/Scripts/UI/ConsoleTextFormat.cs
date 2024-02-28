using TMPro;
using UnityEngine;
using System;
using System.Collections;

public class ConsoleTextFormat : MonoBehaviour
{
	[Header("String format for ")]
	[TextArea(1, int.MaxValue)]
	[SerializeField]
	protected string format;

	[SerializeField]
	private TMP_Text text;

	protected string[] lines;

	[Space]
	[SerializeField]
	private float textWait = 5f;
	[SerializeField]
	private float textWait2 = 5f;
	
	[Space]
	[SerializeField]
	private float textCharDelayMin = 0.00f;
	[SerializeField]
	private float textCharDelayMax = 0.04f;
	[SerializeField]
	private float textCharDelayThreshold = 0.038f;

	[Space]
	[SerializeField]
	private float textLineDelayMin = 0.02f;
	[SerializeField]
	private float textLineDelayMax = 0.08f;

	[Space]
	[SerializeField]
	private bool isAutostart = false;

    public virtual void Start()
	{
		lines = format.Split('\n');
		if (isAutostart) Startup();
	}

	IEnumerator WriteText(Action callback)
	{
		yield return StartCoroutine(WriteText());
		callback();
	}
	IEnumerator WriteText()
	{
		yield return new WaitForSeconds(textWait);

		for (int i = 0; i < lines.Length; i++)
		{
			text.text += '\n';
			for (int j = 0; j < lines[i].Length; j++)
			{
				text.text += lines[i][j];

				float range = UnityEngine.Random.Range(
						textCharDelayMin,
						textCharDelayMax);

				//Sometimes put more than 1 character
				if (range > textCharDelayThreshold) yield return new WaitForSeconds(range);
			}

			//Wait during line
			yield return new WaitForSeconds(
				UnityEngine.Random.Range(
					textLineDelayMin,
					textLineDelayMax));
		}

		yield return new WaitForSeconds(textWait2);
	}

	public void Startup() => StartCoroutine(WriteText());
	public void Startup(Action callback) => StartCoroutine(WriteText(callback));
}
