using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Upgrades;

public class GridButton : MonoBehaviour
{
	Color onColor = Color.green;
	Color offColor = Color.gray;
	public GridSpawner gridSpawner;

	public int x;
	public int y;

	public IntUpgradeData cellValue;
	public FloatUpgradeData cursorRadius;
	SpriteRenderer spriteRenderer;

	public bool isOff;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		UpdateFlip(Random.Range(0, 4) == 0);
	}

	void UpdateFlip(bool isOff)
	{
		this.isOff = isOff;
		spriteRenderer.color = isOff ? offColor : onColor;
	}

	public void TurnOff()
	{
		if (!isOff)
		{
			GameData.EraseBytes(cellValue.CurrentValue);
			UpdateFlip(true);
			gridSpawner.CheckDone();
		}
	}
}
