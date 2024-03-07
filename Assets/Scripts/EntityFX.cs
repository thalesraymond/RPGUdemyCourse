using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("FX")]
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }

    private IEnumerator FlashFx()
    {
        spriteRenderer.material = hitMaterial;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.material = originalMaterial;
    }

    private void RedColorBlink()
    {
        if (spriteRenderer.color != Color.white)
            spriteRenderer.color = Color.white;
        else
            spriteRenderer.color = Color.red;
    }

    private void CancelRedColorBlink()
    {
        this.CancelInvoke();

        this.spriteRenderer.color = Color.white;
    }
}
