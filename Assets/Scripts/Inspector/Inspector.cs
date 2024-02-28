using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Inspector
{
    public class Inspector : MonoBehaviour
    {
        [field: SerializeField]
        private TextMeshProUGUI Text { get; set; }

        public void SetText(string text)
        {
            Text.text = text;
            StopCoroutine(nameof(ClearCoroutine));
        }

        public void Clear()
        {
            StartCoroutine(nameof(ClearCoroutine));
        }

        private IEnumerator ClearCoroutine()
        {
            yield return new WaitForSeconds(1f);
            Text.text = "";
        }
    }
}