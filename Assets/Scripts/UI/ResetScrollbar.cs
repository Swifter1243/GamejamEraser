using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScrollbar : MonoBehaviour
{
    public float value = 1;

    void Start()
    {
        GetComponent<Scrollbar>().value = value;
    }
}
