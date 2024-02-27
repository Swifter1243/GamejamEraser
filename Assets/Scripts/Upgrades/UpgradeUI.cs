using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrades
{
    public class UpgradeUI : MonoBehaviour
    {
        [field: SerializeField]
        public UpgradeData Upgrade { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI Text { get; private set; }

        [field: SerializeField]
        public Button Button { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ButtonText { get; private set; }

        private void Start()
        {
            Text.text = Upgrade.Name;

            Button.onClick.AddListener(() =>
            {
                Upgrade.Level++;
                Refresh();
            });
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            UpdateButtonText();
#if !UNITY_EDITOR
            Button.interactable = !Upgrade.IsMaxLevel && GameData.Currency >= Upgrade.NextCost;
#endif
        }

        private void UpdateButtonText()
        {
            string line1 = $"({Upgrade.Level}/{Upgrade.MaxLevel})";
            string line2 = $"Current: {Upgrade.CurrentName}";
            
            if (!Upgrade.IsMaxLevel)
            {
                string line3 = $"Next: {Upgrade.NextName}";
                ButtonText.text = $"{line1}\n{line2}\n{line3}";
            }
            else
            {
                ButtonText.text = $"{line1}\n{line2}\n.";
            }
        }
    }
}