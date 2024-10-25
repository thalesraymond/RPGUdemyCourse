using Stats;
using UnityEngine;

namespace Inventory.Effects
{
    [CreateAssetMenu(fileName = "BuffEffec", menuName = "Data/Item Effect/Buff Effect")]
    public class BuffEffect : ItemEffect
    {
        private PlayerStats _playerStats;

        [SerializeField] private int _buffAmount;
        [SerializeField] private float _buffDuration;
        [SerializeField] private StatType _type;

        public override void ExecuteEffect(Transform enemyPosition)
        {
            base.ExecuteEffect(enemyPosition);

            _playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

            _playerStats.IncreaseStatBy(_buffAmount, _buffDuration, _playerStats.StatOfType(this._type));
        }
    }
}
