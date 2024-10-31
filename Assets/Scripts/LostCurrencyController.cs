using System;
using Managers;
using PlayerStates;
using SaveAndLoad;
using Stats;
using UnityEngine;

public class LostCurrencyController : MonoBehaviour
{
    public int currencyAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null) return;
        
        PlayerManager.Instance.Currency += currencyAmount;
        
        Destroy(gameObject);
    }
}