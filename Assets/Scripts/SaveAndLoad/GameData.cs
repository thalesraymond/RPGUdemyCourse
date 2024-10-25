namespace SaveAndLoad
{
    [System.Serializable]
    public class GameData
    {
        public int Currency;
        public SerializableDictionary<string, int> InventoryItems;
        public SerializableDictionary<string, bool> SkillTree;

        public GameData()
        {
            this.Currency = 0;

            this.InventoryItems = new SerializableDictionary<string, int>();

            this.SkillTree = new SerializableDictionary<string, bool>();
        }
    }
}
