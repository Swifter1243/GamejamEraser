using UnityEngine;
using Upgrades;

public class Eraser : MonoBehaviour
{
    [field: SerializeField]
    public FloatUpgradeData CursorSize { get; private set; }
    
    [field: SerializeField]
    public Camera Camera { get; private set; }

    private GameStarter GameStarter;

    private void Start()
    {
        GameStarter = FindObjectOfType<GameStarter>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Erase();
        }
    }

    private void Erase()
    {
        Vector2 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.one / 2f;
        Vector2Int floored = new(Mathf.FloorToInt(mousePos.x), Mathf.FloorToInt(mousePos.y));

        foreach (Vector2Int pos in Helpers.GetCircle(floored, CursorSize.CurrentValue))
        {
            if (GameStarter.gridSpawner.buttons.TryGetValue(pos, out GridButton button))
            {
                button.TurnOff();
            }
        }
    }
}