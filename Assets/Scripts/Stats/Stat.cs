using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private int _baseValue;

        public List<int> Modifiers;

        public Stat()
        {
            Modifiers = new List<int>();
        }

        public int GetValue()
        {
            return this._baseValue + this.Modifiers.Sum();
        }

        public void AddModifier(int modifier)
        {
            this.Modifiers.Add(modifier);

            Inventory.Inventory.Instance.UpdateSlotAndStatsUI();
        }

        public void RemoveModifier(int modifier)
        {
            this.Modifiers.Remove(modifier);
        }

    }
}
