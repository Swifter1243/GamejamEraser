using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
	int grid = 4;
	public float sizeAcross = 4;
	public GameObject gridObject;
	public GameStarter gameStarter;

	List<GridButton> buttons = new List<GridButton>();

	public IEnumerable<GridButton> GetAllButtonsOn() => buttons.Where(x => !x.isOff);

	public void TurnRandomButtonOff()
	{
		var list = GetAllButtonsOn();
		int index = Random.Range(0, list.Count());
		list.ToList()[index].TurnOff();
	}

	public void PopulateGrid(int grid)
	{
		this.grid = grid;

		float offset = (grid - 1) / 2f;
		float spacing = sizeAcross / grid;

		for (int x = 0; x < grid; x++)
		{
			for (int y = 0; y < grid; y++)
			{

				var obj = Instantiate(gridObject, transform);
				obj.transform.localPosition = new Vector3((x - offset) * spacing, (y - offset) * spacing, 0);

				obj.transform.localScale = new Vector3(spacing, spacing, 1);

				var button = obj.GetComponent<GridButton>();
				buttons.Add(button);
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
		bool allFlipped = buttons.All(x => x.isOff);

		if (allFlipped)
		{
			Destroy(gameObject);
			gameStarter.MakeGrid();
		}
	}
}
