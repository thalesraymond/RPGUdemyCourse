namespace Enemy.Skeleton
{
    public class SkeletonMoveState : SkeletonGroundedState
    {
        public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemySkeleton) : base(enemyBase, stateMachine, animBoolName, enemySkeleton)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update()
        {
            base.Update();

            this.Enemy.SetVelocity(this.Enemy.MoveSpeed * this.Enemy.FacingDirection, this.Enemy.Rb.velocity.y);

            if (this.Enemy.IsWallDetected() || !Enemy.IsGroundDetected())
            {
                this.Enemy.Flip();
                this.StateMachine.ChangeState(Enemy.IdleState);
            }
        }
    }
}
