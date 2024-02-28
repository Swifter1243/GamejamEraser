using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;

public class ResetHotkey : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R))
        {
            GameData.ResetGame();

            foreach (var upgrade in FindObjectsOfType<UpgradeUI>())
            {
                upgrade.Upgrade.Level = 0;
            }
        }
    }
}