using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridSpawner : MonoBehaviour
{
	public GameObject gridObject;
	public GameStarter gameStarter;

	public GridButton[][] buttons;

	public IEnumerable<GridButton> GetAllButtonsOn() => buttons.SelectMany(b => b).Where(x => !x.isOff);

	public int TurnRandomButtonsOff(int count)
	{
		GridButton[] buttonsOn = GetAllButtonsOn().ToArray();

        if (buttonsOn.Length <= count)
        {
            foreach (GridButton button in buttonsOn)
            {
                button.TurnOff();
            }

            return buttonsOn.Length;
        }

        foreach (GridButton button in buttonsOn.OrderBy(_ => Random.Range(0, int.MaxValue)).Take(count))
        {
            button.TurnOff();
        }

        return count;
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

		if (allFlipped) gameStarter.RemakeGrid();
	}
}
