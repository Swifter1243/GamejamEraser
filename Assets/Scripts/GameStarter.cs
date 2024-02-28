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
    public CameraScaler cameraScaler;

    public AddressFormat addressFormat;

    public static float ANIMATION_TIME = 1;

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
		addressFormat.UpdateText();
		cameraScaler.FixCamera();
        var obj = Instantiate(grid);
        gridSpawner = obj.GetComponent<GridSpawner>();
		gridSpawner.PopulateGrid(gridSize.CurrentValue.x);
		gridSpawner.gameStarter = this;
        StartCoroutine(MakeGridReady());
    }

    IEnumerator MakeGridReady()
    {
        yield return new WaitForSeconds(ANIMATION_TIME);

        foreach (var item in gridSpawner.buttons.Values)
        {
            item.MakeReady();
        }
    }
}
