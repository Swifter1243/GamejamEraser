using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Upgrades;

public class GridButton : MonoBehaviour
{
	Color onColor = Color.green;
	Color offColor = Color.gray;
	public GridSpawner spawner;

	public IntUpgradeData cellValue;

	public bool isOff;

	private void Start()
	{
		UpdateFlip(Random.Range(0, 4) == 0);
	}

	private void OnMouseEnter()
	{
		if (Input.GetMouseButton(0))
		{
			TurnOff();
		}
	}

	private void OnMouseDown()
	{
		TurnOff();
	}

	void UpdateFlip(bool isOff)
	{
		this.isOff = isOff;
		GetComponent<SpriteRenderer>().color = isOff ? offColor : onColor;
	}

	public void TurnOff()
	{
		if (!isOff)
		{
			GameData.EraseBytes(cellValue.CurrentValue);
			UpdateFlip(true);
			spawner.CheckDone();
		}
	}
}
