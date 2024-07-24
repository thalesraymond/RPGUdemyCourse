using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSlotUI : MonoBehaviour
{
    [SerializeField] private string _statName;
    [SerializeField] private TextMeshProUGUI _statValueText;
    [SerializeField] private TextMeshProUGUI _statNameText;
    [SerializeField] private StatType _statType;

    private void Start()
    {
        this.UpdateStatValueUI();
    }
    void OnValidate()
    {
        gameObject.name = "Stat - " + _statName;

        if (_statNameText != null)
        {
            _statNameText.text = _statName;
        }
    }
    public void UpdateStatValueUI()
    {
        var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        if (playerStats == null)
            return;

        _statValueText.text = playerStats.StatOfType(_statType).GetValue().ToString();
    }
}
