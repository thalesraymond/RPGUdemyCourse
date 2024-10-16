using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager Instance;
    public Player Player;

    public int Currency;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            return;
        }

        Instance = this;
    }

    public bool HaveEnoughMoney(int price)
    {
        var hasEnoughMoney = this.Currency >= price;

        if(hasEnoughMoney) {
            Debug.Log("Player has enough money");

            this.Currency -= price;
            return true;
        }

        Debug.Log("Player doesn't have enough money");
        return false;
    }

    public int GetCurrencyAmount() => this.Currency;

    public void LoadData(GameData data)
    {
        this.Currency = data.Currency;
    }

    public void SaveData(ref GameData data)
    {
        data.Currency = this.Currency;
    }
}
