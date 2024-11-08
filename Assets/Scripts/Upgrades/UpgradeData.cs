using System;
using System.Linq;
using UnityEngine;

namespace Upgrades
{
    public class UpgradeData<T> : UpgradeData where T : struct
    {
        public override string CurrentName => Upgrades[Level].Name;
        public override string NextName => NextUpgrade.Name;
        public override int NextCost => NextUpgrade.Cost;
		public override int CurrentCost => Upgrades[Level].Cost;
		public override bool IsMaxLevel => Level == MaxLevel;
        public override int MaxLevel => Upgrades.Length - 1;
        public override int Spent => Upgrades.Skip(1).Take(Level).Sum(level => level.Cost);
        public T CurrentValue => _currentValue ??= Upgrades[Level].Value;
        private T? _currentValue = null;

		public override int Level
        {
            get => PlayerPrefs.GetInt(Key, 0);
            set
            {
                int newLevel = Helpers.AssertRange(value, 0, Upgrades.Length - 1, 0);
                PlayerPrefs.SetInt(Key, newLevel);
                _currentValue = Upgrades[newLevel].Value;
            }
        }

        private Upgrade NextUpgrade => Upgrades[Helpers.AssertRange(Level + 1, 0, MaxLevel, MaxLevel)];

        [field: SerializeField]
        public override string Name { get; protected set; }

        [field: SerializeField] 
        public override string Description { get; protected set; }

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

            [field: SerializeField]
            public int Cost { get; private set; }
        }
    }

    public abstract class UpgradeData : ScriptableObject
    {
        public abstract string Description { get; protected set; }
        public abstract string Name { get; protected set; }
        public abstract string Key { get; protected set; }
        public abstract string CurrentName { get; }
        public abstract string NextName { get; }
        public abstract int NextCost { get; }
		public abstract int CurrentCost { get; }
		public abstract int Level { get; set; }
        public abstract int MaxLevel { get; }
        public abstract bool IsMaxLevel { get; }
        public abstract int Spent { get; }
    }
}