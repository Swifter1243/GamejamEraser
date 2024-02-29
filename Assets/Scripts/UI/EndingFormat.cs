using TMPro;
using UnityEngine;
using System;
using System.Collections;

public class EndingFormat : ConsoleTextFormat
{
    public override void Start()
    {
        lines = string.Format(format, StatsFormat.GetTimeString()).Split('\n');
		Startup();
        GameData.ResetData();
	}
}
