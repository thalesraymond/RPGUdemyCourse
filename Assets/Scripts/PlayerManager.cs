using UnityEngine;

public class PlayerManager : MonoBehaviour
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
}
