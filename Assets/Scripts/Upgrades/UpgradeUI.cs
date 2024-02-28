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
        public Button SellButton { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ButtonText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI CurrentText { get; private set; }

        public RectTransform rt;

		private void Awake()
		{
			rt = GetComponent<RectTransform>();
		}

		private void Start()
        {
            Text.text = Upgrade.Name;

            Button.onClick.AddListener(() =>
            {
                Upgrade.Level++;
                Refresh();
            });

			SellButton.onClick.AddListener(() =>
			{
				Upgrade.Level--;
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
            UpdateAvailability();
            UpdateSellAvailability();
		}

        public Color GetAvailableColor(bool available)
        {
            return available ? Color.white : Color.white * 0.5f;
		}

		public bool GetAvailable()
        {
            return !Upgrade.IsMaxLevel && GameData.Currency >= Upgrade.NextCost;
		}

        public bool GetSellAvailable()
        {
            return Upgrade.Level != 0 && GameData.BytesRemaining > Upgrade.CurrentCost;
        }

        private void UpdateAvailability()
        {
            Button.interactable = GetAvailable();

			var color = GetAvailableColor(GetAvailable());

            Text.color = color;
            Button.GetComponent<TextMeshProUGUI>().color = color;
            ButtonText.color = color;
            CurrentText.color = color;
		}

        private void UpdateSellAvailability()
        {
			SellButton.interactable = GetSellAvailable();
		}

        private void UpdateButtonText()
        {
            CurrentText.text = Upgrade.CurrentName;

            string progress = $"({Upgrade.Level}/{Upgrade.MaxLevel})";

			if (!Upgrade.IsMaxLevel)
            {
                ButtonText.text = $"Next: {Upgrade.NextName}\n" +
                    $"Cost: {Helpers.FormatBytes(Upgrade.NextCost)}\n" +
                    progress;
			}
            else
            {
                ButtonText.text = $"\n\n{progress}";
            }
        }
    }
}