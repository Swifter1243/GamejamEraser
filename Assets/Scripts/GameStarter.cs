using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;

public class GameStarter : MonoBehaviour
{
    public GameObject grid;
    public Vector2IntUpgradeData gridSize;

    public GridSpawner gridSpawner;

    void Start()
    {
        MakeGrid();
    }

    public void MakeGrid()
    {
        var obj = Instantiate(grid);
        gridSpawner = obj.GetComponent<GridSpawner>();
		gridSpawner.PopulateGrid(gridSize.CurrentValue.x);
		gridSpawner.gameStarter = this;
    }
}
