using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [Header("FX")]
    [SerializeField] private Material hitMaterial;
    private Material _originalMaterial;

    [Header("Ailment colors")]
    [SerializeField] private Color _chillColor;
    [SerializeField] private Color[] _igniteColors;
    [SerializeField] private Color[] _shockColors;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _originalMaterial = _spriteRenderer.material;
    }

    private IEnumerator FlashFx()
    {
        _spriteRenderer.material = hitMaterial;

        var currentColor = _spriteRenderer.color;

        _spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.2f);

        _spriteRenderer.material = _originalMaterial;
        _spriteRenderer.color = currentColor;
    }

    public void RedColorBlink()
    {
        if (_spriteRenderer.color != Color.white)
            _spriteRenderer.color = Color.white;
        else
            _spriteRenderer.color = Color.red;
    }

    public void CancelColorChange()
    {
        this.CancelInvoke();

        this._spriteRenderer.color = Color.white;
    }

    public void IgniteFxFor(float seconds)
    {
        InvokeRepeating(nameof(IgniteColorFx), 0, .3f);

        Invoke(nameof(CancelColorChange), seconds);
    }

    public void ShockedFxFor(float seconds)
    {
        InvokeRepeating(nameof(ShockedColorFx), 0, .3f);

        Invoke(nameof(CancelColorChange), seconds);
    }

    public void ChillFxFor(float seconds)
    {
        InvokeRepeating(nameof(ChillColorFx), 0, .3f);

        Invoke(nameof(CancelColorChange), seconds);
    }

    private void IgniteColorFx()
    {
        if (_spriteRenderer.color != _igniteColors[0])
            _spriteRenderer.color = _igniteColors[0];
        else
            _spriteRenderer.color = _igniteColors[1];
    }

    private void ChillColorFx()
    {
        _spriteRenderer.color = this._chillColor;
    }

    private void ShockedColorFx()
    {
        if (_spriteRenderer.color != _shockColors[0])
            _spriteRenderer.color = _shockColors[0];
        else
            _spriteRenderer.color = _shockColors[1];
    }

    public void ToogleTransparent(bool transparent) => this._spriteRenderer.color = transparent ? Color.clear : Color.white;
}
