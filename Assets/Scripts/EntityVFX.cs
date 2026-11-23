using System;
using System.Collections;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Material _originalMaterial;
    private Coroutine _onDamageVFXCoroutine;
    
    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVFXDuration = .2f;

    private void Awake()
    {
        this._spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        this._originalMaterial = this._spriteRenderer.material;
    }
    
    private IEnumerator OnDamageVFXCoroutine()
    {
        if(this._onDamageVFXCoroutine != null) 
            this.StopCoroutine(this._onDamageVFXCoroutine);

        this._spriteRenderer.material = this.onDamageMaterial;
        
        yield return new WaitForSeconds(this.onDamageVFXDuration);
        
        this._spriteRenderer.material = this._originalMaterial;
    }

    public void PlayOnDamageVFX()
    {
        this._onDamageVFXCoroutine = this.StartCoroutine(this.OnDamageVFXCoroutine());
    }
}
