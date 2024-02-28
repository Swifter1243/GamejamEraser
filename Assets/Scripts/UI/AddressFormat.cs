using TMPro;
using UnityEngine;

public class AddressFormat : MonoBehaviour
{
    [SerializeField]
    private TMP_Text addressText;
   
    public void UpdateText()
    {
        string hex = GameData.FreedBytes.ToString("X").PadLeft(8, '0');
        addressText.text = $"0x{hex}";
    }
}
