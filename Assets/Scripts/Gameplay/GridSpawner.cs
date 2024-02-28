using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
	public GameObject gridObject;
	public GameStarter gameStarter;

	public GridButton[][] buttons;

	public IEnumerable<GridButton> GetAllButtonsOn() => buttons.SelectMany(b => b).Where(x => !x.isOff);

	public bool TurnRandomButtonOff()
	{
		var list = GetAllButtonsOn();

		if (list.Count() == 0) return false;

		int index = Random.Range(0, list.Count());
		return list.ToList()[index].TurnOff();
	}

	public void PopulateGrid(int grid)
	{
		buttons = new GridButton[grid][];

		for (int x = 0; x < grid; x++)
        {
            buttons[x] = new GridButton[grid];

			for (int y = 0; y < grid; y++)
			{
				var obj = Instantiate(gridObject, transform);
				obj.transform.localPosition = new Vector2(x, y);

				var button = obj.GetComponent<GridButton>();
				buttons[x][y] = button;
				button.gridSpawner = this;
				button.x = x;
				button.y = y;
				

				// if we want to use UI for some reason
				//var rect = obj.GetComponent<RectTransform>();
				// set width
				//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, spacing);
				// set height
				//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, spacing);
			}
		}
	}

	public void CheckDone()
	{
		bool allFlipped = buttons.SelectMany(b => b).All(x => x.isOff && x.isReady);

		if (allFlipped)
		{
			Destroy(gameObject);
			gameStarter.MakeGrid();
		}
	}
}
