using UnityEngine;
using Upgrades;

public class CameraScaler : MonoBehaviour
{
    [field: SerializeField]
    public Vector2IntUpgradeData GridSizeUpgrade { get; private set; }

    [field: SerializeField]
    public Camera Camera { get; private set; }

    private void Update()
    {
        int size = Mathf.Max(GridSizeUpgrade.CurrentValue.x, GridSizeUpgrade.CurrentValue.y);
        Camera.orthographicSize = size;
        Vector2Int offset = GridSizeUpgrade.CurrentValue / 2;
        Camera.transform.position = new Vector3(offset.x, offset.y, -10);
    }
}