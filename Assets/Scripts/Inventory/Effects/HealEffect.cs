using Managers;
using Stats;
using UnityEngine;

namespace Inventory.Effects
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = "Data/Item Effect/Heal Effect")]
    public class HealEffect : ItemEffect
    {
        [SerializeField][Range(0f, 1f)] private float _healAmountPercent;
        public override void ExecuteEffect(Transform enemyPosition)
        {
            base.ExecuteEffect(enemyPosition);

            var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

            var healAmount = Mathf.RoundToInt(playerStats.MaxHealthPoints.GetValue() * _healAmountPercent);

            playerStats.IncreaseHealthBy(healAmount);
        }
    }
}
