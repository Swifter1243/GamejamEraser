using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Upgrades;

public class GridButton : MonoBehaviour
{
	Color onColor = Color.white;
	Color offColor = Color.black;
	public GridSpawner gridSpawner;

	public int x;
	public int y;

	public IntUpgradeData cellValue;
	public FloatUpgradeData cursorRadius;
	SpriteRenderer spriteRenderer;

	public bool isOff;
	public bool isReady = false;

	[field: SerializeField]
	public FloatUpgradeData AnimationTimeUpgrade { get; private set; }

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		waitTimeMax = Random.Range(0.05f, 0.1f) * 0.3f;
		isOff = Random.Range(0, 4) == 0;
		animationStateColor = StateColor();
	}

	Color StateColor()
	{
		return isOff ? offColor : onColor;
	}

	void UpdateFlip(bool isOff)
	{
		this.isOff = isOff;
		spriteRenderer.color = StateColor();
	}

	float animationTime = 0;
	float waitTime = 0;
	float waitTimeMax;

	Color animationStateColor;
	Color currentRandomColor = Color.black;

	private void Update()
	{
		if (!isReady)
		{
			animationTime += Time.deltaTime;
			waitTime += Time.deltaTime;

			float percentage = animationTime / AnimationTimeUpgrade.CurrentValue;
			spriteRenderer.color = Color.Lerp(currentRandomColor, animationStateColor, percentage);

			if (waitTime > waitTimeMax)
			{
				waitTimeMax *= 1.1f;
				waitTime = 0;
				currentRandomColor = Random.ColorHSV(0, 1, 0, 1 - percentage);
			}
		}
	}

	public bool TurnOff()
	{
		if (!isReady || isOff) return false;

		GameData.EraseBytes(cellValue.CurrentValue);
		UpdateFlip(true);
		gridSpawner.CheckDone();

		//AudioStatics.instance.OnGridOff();

		return true;
	}

	public void MakeReady()
	{
		UpdateFlip(isOff);
		isReady = true;
	}
}
