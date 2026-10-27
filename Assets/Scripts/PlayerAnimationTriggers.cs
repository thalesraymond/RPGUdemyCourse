using System;
using PlayerStateMachine;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        this._player = this.GetComponentInParent<Player>();
    }

    private void CurrentStateTrigger()
    {
        if (this._player != null) this._player.CallAnimationTrigger();
    }
}
