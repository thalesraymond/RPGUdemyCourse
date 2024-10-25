using System.Text;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    public enum ItemType
    {
        Material,
        Equipment
    }

    [CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
    public class ItemData : ScriptableObject
    {
        public string ItemId;
        public ItemType ItemType;

        public string ItemName;

        public Sprite ItemIcon;

        [Range(0, 100)]
        public float DropChance;

        protected StringBuilder sb = new StringBuilder();

        public virtual string GetDescription()
        {
            return string.Empty;
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(this);
            this.ItemId = AssetDatabase.AssetPathToGUID(path);
#endif
        }
    }
}