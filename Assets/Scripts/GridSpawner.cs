using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
	public GameObject gridObject;
	public GameStarter gameStarter;

	bool descending = false;

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

	private void Update()
	{
		if (descending)
		{
			transform.Translate(0, -10 * Time.deltaTime, 0);
		}
	}

	public void PopulateGrid(int grid)
	{
		for (int y = 0; y < grid; y++)
		{
			for (int x = 0; x < grid; x++)
			{
				var obj = Instantiate(gridObject, transform);
				obj.transform.localPosition = new Vector2(x, y);

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
			StartCoroutine(DoAnimation());
			descending = true;
		}
	}

	public IEnumerator DoAnimation()
	{
		yield return new WaitForSeconds(1f);

		Destroy(gameObject);
		gameStarter.MakeGrid();
	}
}
