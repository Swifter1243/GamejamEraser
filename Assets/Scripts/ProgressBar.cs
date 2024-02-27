using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
	[Range(0f, 1f)]
	public float progress;

	public RectTransform bar;

	private void Start()
	{
		UpdateTransform();
	}

	private void OnValidate()
	{
		UpdateTransform();
	}

	void UpdateTransform()
	{
		float width = GetComponent<RectTransform>().sizeDelta.x;
		float right = (1 - progress) * width;
		bar.offsetMax = new Vector2(-right, bar.offsetMax.y);
	}

	void SetProgress(float progress)
	{
		this.progress = progress;
		UpdateTransform();
	}
}
