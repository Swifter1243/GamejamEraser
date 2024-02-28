using UnityEngine;
using Upgrades;

namespace Inspector
{
    public class UpgradeInspectable : Inspectable
    {
        [field: SerializeField]
        public UpgradeUI UI { get; private set; }

        public override string Text => UI.Upgrade.Description;
    }
}