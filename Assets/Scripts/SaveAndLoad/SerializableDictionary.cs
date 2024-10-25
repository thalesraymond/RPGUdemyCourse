using System.Collections.Generic;
using UnityEngine;

namespace SaveAndLoad
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> _keys = new List<TKey>();
        [SerializeField] private List<TValue> _values = new List<TValue>();

        public void OnBeforeSerialize()
        {
            this._keys.Clear();
            this._values.Clear();

            foreach (var item in this)
            {
                this._keys.Add(item.Key);
                this._values.Add(item.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();

            if(this._keys.Count != this._values.Count)
            {
                Debug.LogError("The number of keys and values does not match.");
                return;
            }

            for (var i = 0; i < this._keys.Count; i++)
            {
                this.Add(this._keys[i], this._values[i]);
            }
        }
    }
}
