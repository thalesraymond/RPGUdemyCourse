using UnityEditor;
using UnityEngine;

namespace Enemies.Skeletons
{
    public class SkeletonAttackState : EnemyState
    {
        private readonly EnemySkeleton _enemy;
        public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            this._enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();

            this._enemy.SetVelocityToZero();
        }

        public override void Exit()
        {
            base.Exit();

            this._enemy.LastTimeAttack = Time.time;
        }

        public override void Update()
        {
            base.Update();

            this._enemy.SetVelocityToZero();

            if (this.triggerCalled)
                this.StateMachine.ChangeState(_enemy.BattleState);
        }
    }
}
