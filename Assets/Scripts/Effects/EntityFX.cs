using System.Collections;
using Cinemachine;
using Managers;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    public class EntityFX : MonoBehaviour
    {
        protected SpriteRenderer SpriteRenderer;
    
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

        [Space]
    
        [Header("Popup Text")]
        [SerializeField] private GameObject popUpTextPrefab;

        protected virtual void Start()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

            _originalMaterial = SpriteRenderer.material;
        }
    
        public void CreatePopUpText(string text) => CreatePopUpText(text, Color.white);

        public void CreatePopUpText(string text, Color color)
        {
            var randomX = Random.Range(-1, 1);
            var randomY = Random.Range(1, 3);
        
            var positionOffset = new Vector3(randomX, randomY, 0);
        
            var newText = Instantiate(this.popUpTextPrefab, transform.position + positionOffset, Quaternion.identity);
        
            newText.GetComponent<TextMeshPro>().text = text;
        
            newText.GetComponent<TextMeshPro>().color = color;
        }

        public IEnumerator FlashFx()
        {
            SpriteRenderer.material = hitMaterial;

            var currentColor = SpriteRenderer.color;

            SpriteRenderer.color = Color.white;

            yield return new WaitForSeconds(0.2f);

            SpriteRenderer.material = _originalMaterial;
            SpriteRenderer.color = currentColor;
        }

        public void RedColorBlink()
        {
            SpriteRenderer.color = SpriteRenderer.color != Color.white ? Color.white : Color.red;
        }

        public void CancelColorChange()
        {
            this.CancelInvoke();

            this.SpriteRenderer.color = Color.white;
        
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
            SpriteRenderer.color = SpriteRenderer.color != _igniteColors[0] ? _igniteColors[0] : _igniteColors[1];
        }

        private void ChillColorFx()
        {
            SpriteRenderer.color = this._chillColor;
        }

        private void ShockedColorFx()
        {
            SpriteRenderer.color = SpriteRenderer.color != _shockColors[0] ? _shockColors[0] : _shockColors[1];
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

        public void ToogleTransparent(bool transparent) => this.SpriteRenderer.color = transparent ? Color.clear : Color.white;
    }
}
