using System;
using UnityEngine;

namespace Upgrades
{
    public class UpgradeData<T> : ScriptableObject
    {
        public string CurrentName => Upgrades[Level].Name;
        public T CurrentValue => Upgrades[Level].Value;

        public int Level
        {
            get => PlayerPrefs.GetInt(Key, 0);
            set => PlayerPrefs.SetInt(Key, Helpers.AssertRange(value, 0, Upgrades.Length - 1, 0));
        }

        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public string Key { get; private set; }

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
}
