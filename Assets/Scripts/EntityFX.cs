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
    
    [Header("Ailment particles")]
    [SerializeField] private ParticleSystem igniteParticleFX;
    [SerializeField] private ParticleSystem shockParticleFX;
    [SerializeField] private ParticleSystem chillParticleFX;

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
        _spriteRenderer.color = _spriteRenderer.color != Color.white ? Color.white : Color.red;
    }

    public void CancelColorChange()
    {
        this.CancelInvoke();

        this._spriteRenderer.color = Color.white;
        
        this.igniteParticleFX.Stop();
        this.shockParticleFX.Stop();
        this.chillParticleFX.Stop();
    }

    public void IgniteFxFor(float seconds)
    {
        this.igniteParticleFX.Play();
        
        InvokeRepeating(nameof(IgniteColorFx), 0, .3f);

        Invoke(nameof(CancelColorChange), seconds);
    }

    public void ShockedFxFor(float seconds)
    {
        this.shockParticleFX.Play();
        
        InvokeRepeating(nameof(ShockedColorFx), 0, .3f);

        Invoke(nameof(CancelColorChange), seconds);
    }

    public void ChillFxFor(float seconds)
    {
        this.chillParticleFX.Play();
        
        InvokeRepeating(nameof(ChillColorFx), 0, .3f);

        Invoke(nameof(CancelColorChange), seconds);
    }

    private void IgniteColorFx()
    {
        _spriteRenderer.color = _spriteRenderer.color != _igniteColors[0] ? _igniteColors[0] : _igniteColors[1];
    }

    private void ChillColorFx()
    {
        _spriteRenderer.color = this._chillColor;
    }

    private void ShockedColorFx()
    {
        _spriteRenderer.color = _spriteRenderer.color != _shockColors[0] ? _shockColors[0] : _shockColors[1];
    }

    public void ToogleTransparent(bool transparent) => this._spriteRenderer.color = transparent ? Color.clear : Color.white;
}
