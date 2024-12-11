namespace Enemies.Skeletons
{
    public class SkeletonMoveState : SkeletonGroundedState
    {
        public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemySkeleton) : base(enemyBase, stateMachine, animBoolName, enemySkeleton)
        {
        }

        public override void Update()
        {
            base.Update();

            this.Enemy.SetVelocity(this.Enemy.MoveSpeed * this.Enemy.FacingDirection, this.Enemy.Rb.linearVelocity.y);

            if (this.Enemy.IsWallDetected() || !Enemy.IsGroundDetected())
            {
                this.Enemy.Flip();
                this.StateMachine.ChangeState(Enemy.IdleState);
            }
        }
    }
}
