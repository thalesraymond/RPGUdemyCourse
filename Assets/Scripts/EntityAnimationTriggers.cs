using System;
using PlayerStateMachine;
using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity _entity;
    private EntityCombat _entityCombat;

    private void Awake()
    {
        this._entity = this.GetComponentInParent<Entity>();
        this._entityCombat = this.GetComponentInParent<EntityCombat>();
    }

    private void CurrentStateTrigger()
    {
        if (this._entity != null) this._entity.CalCurrentStateAnimationTrigger();
    }

    private void AttackTrigger()
    {
        this._entityCombat.PerformAttack();
    }
}