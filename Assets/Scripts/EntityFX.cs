using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("FX")]
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;

    [Header("Ailment colors")]
    [SerializeField] private Color _chillColor;
    [SerializeField] private Color[] _igniteColors;
    [SerializeField] private Color[] _shockColors;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }

    private IEnumerator FlashFx()
    {
        spriteRenderer.material = hitMaterial;

        var currentColor = spriteRenderer.color;

        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.material = originalMaterial;
        spriteRenderer.color = currentColor;
    }

    public void RedColorBlink()
    {
        if (spriteRenderer.color != Color.white)
            spriteRenderer.color = Color.white;
        else
            spriteRenderer.color = Color.red;
    }

    public void CancelColorChange()
    {
        this.CancelInvoke();

        this.spriteRenderer.color = Color.white;
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
        if(spriteRenderer.color != _igniteColors[0])
            spriteRenderer.color = _igniteColors[0];
        else
            spriteRenderer.color = _igniteColors[1];
    }

    private void ChillColorFx()
    {
        spriteRenderer.color = this._chillColor;
    }

    private void ShockedColorFx()
    {
        if (spriteRenderer.color != _shockColors[0])
            spriteRenderer.color = _shockColors[0];
        else
            spriteRenderer.color = _shockColors[1];
    }
}
