using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;

public class QuitScript : MonoBehaviour
{
    void Update()
    {
        //Quit
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
}
