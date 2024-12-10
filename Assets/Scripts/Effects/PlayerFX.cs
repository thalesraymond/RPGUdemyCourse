using Cinemachine;
using Managers;
using Unity.Mathematics;
using UnityEngine;

namespace Effects
{
    public class PlayerFX : EntityFX
    {
        [Header("After Image FX")] 
        [SerializeField] private GameObject afterImagePrefab;
        [SerializeField] private float colorLooseRate;
        [SerializeField] private float afterImageCooldown;
        [SerializeField] private float afterImageTimer;
        
        [Header("Camera Shake")]
        [SerializeField] private CinemachineImpulseSource screenShake;
        [SerializeField] private float shakeMultiplier;
        [SerializeField] public Vector3 shakeSwordImpact;
        [SerializeField] public Vector3 shakeHighDamage;
        
        [SerializeField] private ParticleSystem dustFX;
        
        private void Update()
        {
            afterImageTimer -= Time.deltaTime;
        }

        protected override void Start()
        {
            base.Start();
            
            screenShake = GetComponent<CinemachineImpulseSource>();
        }
        
        public void ScreenShake(Vector3 shakePower)
        {   
            screenShake.m_DefaultVelocity = new Vector3(shakePower.x * PlayerManager.Instance.Player.FacingDirection, shakePower.y) * shakeMultiplier;
        
            screenShake.GenerateImpulse();
        }
        
        public void PlayDustFX()
        {
            if (this.dustFX == null)
                return;
        
            this.dustFX.Play();
        }

        public void CreateAfterImage()
        {
            if (!(afterImageTimer < 0)) return;
        
            afterImageTimer = afterImageCooldown;
            
            var newAfterImage = Instantiate(this.afterImagePrefab, transform.position, quaternion.identity);
        
            if(PlayerManager.Instance.Player.FacingDirection == -1)
                newAfterImage.transform.Rotate(0, 180, 0);

            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(this.colorLooseRate, this.SpriteRenderer.sprite);
        }
    }
}