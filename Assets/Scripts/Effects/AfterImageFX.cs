using UnityEngine;

namespace Effects
{
    public class AfterImageFX : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private float _colorLooseRate;

        public void SetupAfterImage(float losingSpeed, Sprite spriteImage)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _spriteRenderer.sprite = spriteImage;

            _colorLooseRate = losingSpeed;


        }

        private void Update()
        {
            var alpha = _spriteRenderer.color.a - _colorLooseRate * Time.deltaTime;
        
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, alpha);
        
            if(_spriteRenderer.color.a <= 0)
                Destroy(gameObject);
        }
    }
}
