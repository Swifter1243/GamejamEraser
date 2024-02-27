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

		float radius = cursorRadius.CurrentValue;

		if (radius > 1)
		{
			var inCircle = Helpers.GetCircle(new Vector2Int(x, y), radius);

            foreach (var item in inCircle)
            {
                var success = gridSpawner.buttons.TryGetValue(item, out  var button);

				if (success) button.TurnOff();
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
