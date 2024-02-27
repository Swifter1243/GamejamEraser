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
            UpdateButtonText();

            Button.onClick.AddListener(() =>
            {
                Upgrade.Level++;
                UpdateButtonText();
            });
        }

        private void UpdateButtonText()
        {
            ButtonText.text = Upgrade.Level.ToString();
        }
    }
}
