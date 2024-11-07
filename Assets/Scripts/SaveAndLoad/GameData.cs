// ReSharper disable InconsistentNaming
// Unity doesn't serialize properties with get;set, DO NOT "FIX" this

using System;

namespace SaveAndLoad
{
    [Serializable]
    public class GameData
    {
        public int Currency = 0;
        public SerializableDictionary<string, int> InventoryItems = new();
        
        public SerializableDictionary<string, bool> SkillTree = new();

        public SerializableDictionary<string, bool> Checkpoints = new();

        public string ClosestCheckpointId = string.Empty;

        public float bodyLocationX;
        public float bodyLocationY;
        public int lostCurrency;

        public SerializableDictionary<string, float> VolumeSettings = new();

    }
}
