using TMPro;
using UnityEngine;
using System;
using System.Collections;

public class StartupFormat : ConsoleTextFormat
{
    public override void Start()
    {
        DateTime time = DateTime.Now;
        lines = string.Format(format,
            time.DayOfWeek.ToString().Substring(0, 3),
            2, //febuary
            20 + (int)time.DayOfWeek, //20th + day of week
            1994, //year 1994
            time.Hour,
            time.Minute,
            time.Second,
            time.Millisecond / 10
            ).Split('\n');
    }
}
