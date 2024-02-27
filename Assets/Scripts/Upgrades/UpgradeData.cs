using System;
using UnityEngine;

namespace Upgrades
{
    public class UpgradeData<T> : UpgradeData
    {
        public override string CurrentName => Upgrades[Level].Name;
        public override string NextName => Upgrades[Helpers.AssertRange(Level + 1, 0, MaxLevel, MaxLevel)].Name;
        public override bool IsMaxLevel => Level == MaxLevel;
        public override int MaxLevel => Upgrades.Length - 1;
        public T CurrentValue => Upgrades[Level].Value;

        public override int Level
        {
            get => PlayerPrefs.GetInt(Key, 0);
            set => PlayerPrefs.SetInt(Key, Helpers.AssertRange(value, 0, Upgrades.Length - 1, 0));
        }

        [field: SerializeField]
        public override string Name { get; protected set; }

        [field: SerializeField]
        public override string Key { get; protected set; }

        [field: SerializeField]
        public Upgrade[] Upgrades { get; private set; }

        [Serializable]
        public class Upgrade
        {
            [field: SerializeField]
            public string Name { get; private set; }

            [field: SerializeField]
            public T Value { get; private set; }
        }
    }

    public abstract class UpgradeData : ScriptableObject
    {
        public abstract string Name { get; protected set; }
        public abstract string Key { get; protected set; }
        public abstract string CurrentName { get; }
        public abstract string NextName { get; }
        public abstract int Level { get; set; }
        public abstract int MaxLevel { get; }
        public abstract bool IsMaxLevel { get; }
    }
}
