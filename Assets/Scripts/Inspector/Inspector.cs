using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Inspector
{
    public class Inspector : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI Text { get; private set; }
    }
}