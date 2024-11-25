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
    
    [Header("Hit FX")]
    [SerializeField] private GameObject hitFX;
    [SerializeField] private GameObject criticalHitFX;

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

    public void CreateHitFX(Transform target, bool isCriticalHit)
    {
        var randomZRotation = Random.Range(-90, 90);
        var randomXPosition = Random.Range(-0.3f, 0.3f);
        var randomYPosition = Random.Range(-0.3f, 0.3f);
        
        var hitFXRotation = new Vector3(0, 0, randomZRotation);
        
        
        var newHitFX = Instantiate(isCriticalHit ? criticalHitFX : hitFX, target.position + new Vector3(randomXPosition, randomYPosition), Quaternion.identity);
        
        if (isCriticalHit)
        {
            var yRotation = 0f;

            var facingDirection = GetComponent<Entity>().FacingDirection;

            randomZRotation = Random.Range(-45, 45);

            if (facingDirection == -1)
                yRotation = 180f;

            hitFXRotation = new Vector3(0, yRotation, randomZRotation);
        }
        
        newHitFX.transform.Rotate(hitFXRotation);

        Destroy(newHitFX, 0.5f);
    }

    public void ToogleTransparent(bool transparent) => this._spriteRenderer.color = transparent ? Color.clear : Color.white;
}
