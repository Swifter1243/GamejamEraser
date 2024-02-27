using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
	public int width = 4;
	public int height = 4;
	public GameObject gridObject;

	private void Start()
	{
		PopulateGrid();
	}

	void PopulateGrid()
	{
		float offsetX = (width - 1) / 2f;
		float offsetY = (height - 1) / 2f;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var obj = Instantiate(gridObject, transform);
				obj.transform.position = new Vector3(x - offsetX, y - offsetY, 0);
			}
		}
	}
}
