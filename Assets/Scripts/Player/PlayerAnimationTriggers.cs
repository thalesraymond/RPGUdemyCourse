using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            hit.GetComponent<Enemy>().Damage();
    }
}
