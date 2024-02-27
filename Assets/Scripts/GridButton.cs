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
	SpriteRenderer renderer;

	public bool isOff;

	private void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
		UpdateFlip(Random.Range(0, 4) == 0);
	}

	private void OnMouseEnter()
	{
		if (Input.GetMouseButton(0))
		{
			ClickedTurnOff();
		}
	}

	private void OnMouseDown()
	{
		ClickedTurnOff();
	}

	void UpdateFlip(bool isOff)
	{
		this.isOff = isOff;
		renderer.color = isOff ? offColor : onColor;
	}

	public void ClickedTurnOff()
	{
		TurnOff();

		if (cursorRadius.CurrentValue > 1)
		{
            foreach (var item in gridSpawner.GetAllButtonsOn())
            {
				float deltaX = item.x - x;
				float deltaY = item.y - y;
				float dist = deltaX * deltaX + deltaY * deltaY;

				if (dist <= cursorRadius.CurrentValue * cursorRadius.CurrentValue)
				{
					item.TurnOff();
				}
            }
        }
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
