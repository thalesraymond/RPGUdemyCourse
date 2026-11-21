using UnityEngine;

namespace Enemies
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) : base(enemyObject, stateMachine, animationBoolName)
        {
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            
            if(nextState != null)
                return nextState;

            return this.TriggerCalled 
                ? this.EnemyObject.BattleState 
                : null;
        }
    }
}