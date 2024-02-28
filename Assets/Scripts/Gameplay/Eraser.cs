using UnityEngine;
using Upgrades;

public class Eraser : MonoBehaviour
{
    [field: SerializeField]
    public FloatUpgradeData CursorSize { get; private set; }

	[field: SerializeField]
	public IntUpgradeData CellValue { get; private set; }

	[field: SerializeField]
    public Camera Camera { get; private set; }

    private GameStarter GameStarter;
    private Vector2Int? LastClickPos;

    private void Start()
    {
        GameStarter = FindObjectOfType<GameStarter>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.one / 2f;
            Vector2Int floored = mousePos.ToInt();

            EraseAt(floored);

            if (LastClickPos is { } lastPos)
            {
                float d = Vector2Int.Distance(floored, lastPos);

                for (int i = 0; i < d; i++)
                {
                    EraseAt(Vector2.Lerp(lastPos, floored, i / d).ToInt());
                }
            }

            LastClickPos = floored;
        }
        else
        {
            LastClickPos = null;
        }
    }

    private void EraseAt(Vector2Int position)
    {
        foreach (Vector2Int pos in Helpers.GetCircle(position, CursorSize.CurrentValue))
        {
            GridButton[][] buttons = GameStarter.gridSpawner.buttons;

            Vector2Int size = new(buttons.Length, buttons[0].Length);

            if (pos is { x: >= 0, y: >= 0 } && pos.x < size.x && pos.y < size.y)
            {
                buttons[pos.x][pos.y].TurnOff();
            }
        }
    }
}