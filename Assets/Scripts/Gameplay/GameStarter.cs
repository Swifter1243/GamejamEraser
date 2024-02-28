using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	[field: SerializeField]
	public FloatUpgradeData AnimationTimeUpgrade { get; private set; }

	[field: SerializeField]
	public IntUpgradeData CellValue { get; private set; }

	void Awake()
    {
        MakeGrid();
    }

	private IEnumerator Start()
	{
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            gridSpawner.TurnRandomButtonsOff(idleRate.CurrentValue / 2);
        }
	}

    public void RemakeGrid()
    {
        Destroy(gridSpawner.gameObject);
        MakeGrid();
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
        yield return new WaitForSeconds(AnimationTimeUpgrade.CurrentValue);

        foreach (var item in gridSpawner.buttons.SelectMany(b => b))
        {
            item.MakeReady();
        }
    }
}
