using UnityEngine;

namespace Enemies
{
    public class EnemyBattleState : EnemyState
    {
        public EnemyBattleState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) : base(enemyObject, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("enter battle state");
        }

        public override EntityState Update()
        {
            return base.Update();
        }
    }
}