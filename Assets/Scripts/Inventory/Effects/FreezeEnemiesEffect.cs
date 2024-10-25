using System.Linq;
using Enemies;
using Stats;
using UnityEngine;

namespace Inventory.Effects
{
    [CreateAssetMenu(fileName = "FreezeEnemiesEffect", menuName = "Data/Item Effect/Freeze Enemies Effect")]
    public class FreezeEnemiesEffect : ItemEffect
    {
        [SerializeField] private float _duration;

        public override void ExecuteEffect(Transform transform)
        {
            if (!Inventory.Instance.CanUseArmor())
                return;

            Debug.Log("Start Freeze Enemies Effect executed");

            var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();


            if (playerStats.CurrentHealthPoints > playerStats.MaxHealthPoints.GetValue() * .20f)
                return;

            var enemyColliders = Physics2D
                .OverlapCircleAll(transform.position, 2)
                .Where(hit => hit.GetComponent<Enemy>() is not null);

            foreach (var hit in enemyColliders)
            {
                hit.GetComponent<Enemy>().StartFreezeTimeForCoroutine(this._duration);
            }
        }
    }
}
