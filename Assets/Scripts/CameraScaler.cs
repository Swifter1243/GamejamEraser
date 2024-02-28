using UnityEngine;
using Upgrades;

public class CameraScaler : MonoBehaviour
{
    [field: SerializeField]
    public Vector2IntUpgradeData GridSizeUpgrade { get; private set; }
    const float GRID_SIZE_SCALE = 5f / 4f;

    public Vector2 GridOffset;

    [field: SerializeField]
    public Camera Camera { get; private set; }

    public void FixCamera()
    {
        float size = Mathf.Max(GridSizeUpgrade.CurrentValue.x, GridSizeUpgrade.CurrentValue.y) * GRID_SIZE_SCALE;
		Camera.orthographicSize = size;
		Vector2 offset = GridSizeUpgrade.CurrentValue / 2 - GridOffset * GridSizeUpgrade.CurrentValue;
		Camera.transform.position = new Vector3(offset.x - 0.5f, offset.y - 0.5f, -10);
	}
}