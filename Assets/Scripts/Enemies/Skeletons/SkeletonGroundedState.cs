using UnityEngine;

namespace Enemies.Skeletons
{
    public class SkeletonGroundedState : EnemyState
    {
        protected EnemySkeleton Enemy;

        protected Transform Player;
        public SkeletonGroundedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            this.Enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();

            this.Player = PlayerManager.Instance.Player.transform;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (this.Enemy.IsPlayerDetected() || Vector2.Distance(this.Enemy.transform.position, this.Player.position) < 2)
                this.StateMachine.ChangeState(this.Enemy.BattleState);
        }
    }
}
