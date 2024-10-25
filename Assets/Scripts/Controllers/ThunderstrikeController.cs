using Enemies;
using Stats;
using UnityEngine;

namespace Controllers
{
    public class ThunderStrikeController : MonoBehaviour
    {
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Enemy>() == null) return;

            var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

            var enemy = other.GetComponent<Enemy>();

            playerStats.DoMagicalDamage(enemy.GetComponent<EnemyStats>());
        }
    }
}
