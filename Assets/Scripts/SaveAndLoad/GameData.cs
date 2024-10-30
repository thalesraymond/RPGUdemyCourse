// ReSharper disable InconsistentNaming
// Unity doesn't serialize properties with get;set, DO NOT "FIX" this
namespace SaveAndLoad
{
    [System.Serializable]
    public class GameData
    {
        public int Currency = 0;
        public SerializableDictionary<string, int> InventoryItems = new();
        
        public SerializableDictionary<string, bool> SkillTree = new();

        public SerializableDictionary<string, bool> Checkpoints = new();

        public string ClosestCheckpointId = string.Empty;
    }
}
