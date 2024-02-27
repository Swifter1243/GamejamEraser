using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
	public int grid = 4;
	public float sizeAcross = 4;
	public GameObject gridObject;

	private void Start()
	{
		PopulateGrid();
	}

	void PopulateGrid()
	{
		float offset = (grid - 1) / 2f;
		float spacing = sizeAcross / grid;

		for (int x = 0; x < grid; x++)
		{
			for (int y = 0; y < grid; y++)
			{

				var obj = Instantiate(gridObject, transform);
				obj.transform.localPosition = new Vector3((x - offset) * spacing, (y - offset) * spacing, 0);

				obj.transform.localScale = new Vector3(spacing, spacing, 1);

				// if we want to use UI for some reason
				//var rect = obj.GetComponent<RectTransform>();
				// set width
				//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, spacing);
				// set height
				//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, spacing);
			}
		}
	}
}
