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
}
