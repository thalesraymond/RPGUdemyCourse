using System.Linq;
using Enemies;
using Inventory;
using Skills;
using Stats;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player player => this.GetComponentInParent<Player>();

        private void AnimationTrigger()
        {
            this.player.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.AttackCheckRadius)
                .Where(hit => hit.GetComponent<Enemy>() is not null);

            foreach (var hit in colliders)
            {
                var targetStats = hit.GetComponent<EnemyStats>();

                this.player.Stats.DoDamage(targetStats);

                var equippedWeapon = Inventory.Inventory.Instance.GetEquipmentByType(EquipmentType.Weapon);

                if (equippedWeapon != null)
                    equippedWeapon.ExecuteItemEffect(targetStats.transform);
            }
        }

        private void ThrowSword()
        {
            SkillManager.Instance.SwordSkill.CreateSword();
        }
    }
}
