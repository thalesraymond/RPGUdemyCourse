using UnityEngine;

namespace Inventory.Effects
{
    [CreateAssetMenu(fileName = "IceAndFireEffect", menuName = "Data/Item Effect/Ice and Fire")]
    public class IceAndFireEffect : ItemEffect
    {
        [SerializeField] private GameObject _iceAndFirePrefab;
        [SerializeField] private Vector2 _velocity;

        [SerializeField] private float _maxTravelTime;

        public override void ExecuteEffect(Transform respawnPosition)
        {
            var playerTransform = PlayerManager.Instance.Player.transform;

            var comboCounter = PlayerManager.Instance.Player.PrimaryAttackState.ComboCounter;

            if (comboCounter < 2)
                return;

            var newIceAndFire = Instantiate(_iceAndFirePrefab, respawnPosition.position, playerTransform.rotation);

            newIceAndFire.GetComponent<Rigidbody2D>().velocity = _velocity * PlayerManager.Instance.Player.FacingDirection;

            // destroy when traveled a certain distance

            Destroy(newIceAndFire, _maxTravelTime);
        }
    }
}
