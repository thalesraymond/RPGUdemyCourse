using System.Linq;
using PlayerStates;
using Stats;
using UnityEngine;

namespace Enemies.Skeletons
{
    public class EnemySkeletonAnimationTriggers : MonoBehaviour
    {
        private EnemySkeleton enemy => GetComponentInParent<EnemySkeleton>();

        private void AnimationTrigger()
        {
            this.enemy.AnimationFinishTrigger();
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(enemy.AttackCheck.position, enemy.AttackCheckRadius)
                .Where(hit => hit.GetComponent<Player>() is not null);

            foreach (var hit in colliders)
            {
                this.enemy.Stats.DoDamage(hit.GetComponent<PlayerStats>());
            }

        }

        private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

        private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
    }
}
