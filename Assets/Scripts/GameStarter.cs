using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;

public class GameStarter : MonoBehaviour
{
    public GameObject grid;
    public Vector2IntUpgradeData gridSize;
    public IntUpgradeData idleRate;

    public GridSpawner gridSpawner;

    void Awake()
    {
        MakeGrid();
    }

    float rateAddup = 0;

	private void Update()
	{
        rateAddup += Time.deltaTime * idleRate.CurrentValue;
        
        while (rateAddup >= 1) {
            rateAddup -= 1;
            gridSpawner.TurnRandomButtonOff();
        }
	}

	public void MakeGrid()
    {
        var obj = Instantiate(grid);
        gridSpawner = obj.GetComponent<GridSpawner>();
		gridSpawner.PopulateGrid(gridSize.CurrentValue.x);
		gridSpawner.gameStarter = this;
    }
}
