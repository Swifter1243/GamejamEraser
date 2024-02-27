using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [field: SerializeField]
    public TextMeshProUGUI Text { get; private set; }

    [field: SerializeField]
    public Slider Slider { get; private set; }
}