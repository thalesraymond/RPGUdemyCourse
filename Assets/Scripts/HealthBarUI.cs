using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity _entity;

    private RectTransform _rectTransform;

    private Slider _slider;

    private CharacterStats _stats;

    private void Start()
    {
        this._rectTransform = GetComponent<RectTransform>();

        this._entity = GetComponentInParent<Entity>();

        this._slider = GetComponentInChildren<Slider>();

        this._entity.OnFlipped += FlipIU;

        this._stats = GetComponentInParent<CharacterStats>();

        this._stats.OnHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        _slider.maxValue = _stats.GetMaxHealthValue();
        _slider.value = _stats.CurrentHealthPoints;
    }

    private void FlipIU() => this._rectTransform.Rotate(0, 180, 0);
    private void OnDisable()
    {
        _entity.OnFlipped -= FlipIU;
        _stats.OnHealthChanged -= UpdateHealthUI;
    }
}
