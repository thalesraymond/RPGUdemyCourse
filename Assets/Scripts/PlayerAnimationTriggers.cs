using PlayerStateMachine;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    public void AttackOver()
    {
        var player = this.GetComponentInParent<Player>();
        
        if (player != null)
        {
            player.CallAnimationTrigger();
        }
    }
}
