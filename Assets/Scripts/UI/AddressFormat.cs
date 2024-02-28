using TMPro;
using UnityEngine;

public class AddressFormat : MonoBehaviour
{
    [SerializeField]
    private TMP_Text addressText;
    [Header("Updated when next grid, string requires one integer.")]
    [TextArea]
    public string formatString;

    public int Address
    {
        set { addressText.text = string.Format(formatString, value); }
    }
}
