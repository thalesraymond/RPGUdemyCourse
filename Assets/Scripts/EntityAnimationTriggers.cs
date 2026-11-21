using System;
using PlayerStateMachine;
using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity _entity;

    private void Awake()
    {
        this._entity = this.GetComponentInParent<Entity>();
    }

    private void CurrentStateTrigger()
    {
        if (this._entity != null) this._entity.CalCurrentStateAnimationTrigger();
    }
}