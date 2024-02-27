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

	public Dictionary<Vector2Int, GridButton> buttons = new Dictionary<Vector2Int, GridButton>();

	public IEnumerable<GridButton> GetAllButtonsOn() => buttons.Values.Where(x => !x.isOff);

	public bool TurnRandomButtonOff()
	{
		var list = GetAllButtonsOn();

		if (list.Count() == 0) return false;

		int index = Random.Range(0, list.Count());
		list.ToList()[index].TurnOff();
		return true;
	}

	public void PopulateGrid(int grid)
	{
		this.grid = grid;

		float offset = (grid - 1) / 2f;
		float spacing = sizeAcross / grid;

		for (int y = 0; y < grid; y++)
		{
			for (int x = 0; x < grid; x++)
			{
				var obj = Instantiate(gridObject, transform);
				obj.transform.localPosition = new Vector3((x - offset) * spacing, (y - offset) * spacing, 0);

				obj.transform.localScale = new Vector3(spacing, spacing, 1);

				var button = obj.GetComponent<GridButton>();
				buttons[new Vector2Int(x, y)] = button;
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
		bool allFlipped = buttons.Values.All(x => x.isOff);

		if (allFlipped)
		{
			Destroy(gameObject);
			gameStarter.MakeGrid();
		}
	}
}
