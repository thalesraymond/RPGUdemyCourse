using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonAttackState : EnemyState
    {
        private EnemySkeleton enemy;
        public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();

            this.enemy.SetVelocityToZero();
        }

        public override void Exit()
        {
            base.Exit();

            this.enemy.LastTimeAttack = Time.time;
        }

        public override void Update()
        {
            base.Update();

            this.enemy.SetVelocityToZero();

            if (this.triggerCalled)
                this.StateMachine.ChangeState(enemy.BattleState);
        }
    }
}
