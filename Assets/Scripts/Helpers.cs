using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static int AssertRange(int value, int lower, int upper, int fallback)
    {
        if (value < lower || value > upper)
        {
            Debug.LogError($"Invalid value {value}. Expected {lower}-{upper}.");
            return fallback;
        }

        return value;
    }

    public static IEnumerable<Vector2Int> GetCircle(Vector2Int centre, float radius)
    {
        int radiusCeil = Mathf.CeilToInt(radius);
        float sqrRadius = radius * radius;

        for (int x = centre.x - radiusCeil; x <= centre.x + radiusCeil; x++)
        {
            for (int y = centre.y - radiusCeil; y <= centre.y + radiusCeil; y++)
            {
                var v2 = new Vector2Int(x, y);

                if ((v2 - centre).sqrMagnitude <= sqrRadius)
                {
                    yield return v2;
                }
            }
        }
    }

    // Int <-> Float
    public static Vector2Int ToInt(this Vector2 v2) => new((int) v2.x, (int) v2.y);
    public static Vector2 ToFloat(this Vector2Int v2) => v2;
    public static Vector3Int ToInt(this Vector3 v3) => new((int) v3.x, (int) v3.y, (int) v3.z);
    public static Vector3 ToFloat(this Vector3Int v3) => v3;

    // 2D <-> 3D
    public static Vector2Int To2D(this Vector3Int v3) => (Vector2Int) v3;
    public static Vector3Int To3D(this Vector2Int v2, int z = 0) => new(v2.x, v2.y, z);
    public static Vector2 To2D(this Vector3 v3) => v3;
    public static Vector3 To3D(this Vector2 v2, float z = 0f) => new(v2.x, v2.y, z);

    public static string[] byteUnits = new string[] {"b", "kB", "mb", "gb", "tb" };

    public static string FormatBytes(int bytes)
    {
        int unit = 0;

        while (bytes >= 1000)
        {
			bytes /= 1000;
            unit++;
        }

        return $"{bytes}{byteUnits[unit]}";
    }
}