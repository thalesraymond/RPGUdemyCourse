using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            hit.GetComponent<Player>().Damage();
    }
}
