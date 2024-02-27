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

        for (int x = -radiusCeil; x <= radiusCeil; x++)
        {
            for (int y = -radiusCeil; y <= radiusCeil; y++)
            {
                var v2 = new Vector2Int(x, y);

                if ((v2 - centre).sqrMagnitude <= sqrRadius)
                {
                    yield return v2;
                }
            }
        }
    }
}
