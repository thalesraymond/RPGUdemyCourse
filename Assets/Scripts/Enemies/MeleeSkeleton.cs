using System;
using UnityEngine;

namespace Enemies
{
    public class MeleeSkeleton : Enemy
    {

        protected override void Start()
        {
            base.Start();
            this.StateMachine.Initialize(this.EnemyIdleState);
        }
        
    }
}