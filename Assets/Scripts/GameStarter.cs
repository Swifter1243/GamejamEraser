using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject grid;

    int testVal = 4;

    void Start()
    {
        MakeGrid();
    }

    public void MakeGrid()
    {
        var obj = Instantiate(grid);
        var spawner = obj.GetComponent<GridSpawner>();
        spawner.PopulateGrid(testVal);
        spawner.gameStarter = this;
		testVal++;
    }
}
