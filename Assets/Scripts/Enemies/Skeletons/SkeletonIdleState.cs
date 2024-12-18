using Managers;

namespace Enemies.Skeletons
{
    public class SkeletonIdleState : SkeletonGroundedState
    {
        public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this.StateTimer = this.Enemy.IdleTime;
        }

        public override void Exit()
        {
            base.Exit();
            
            AudioManager.Instance.PlaySoundEffect(SoundEffect.SkeletonBones, this.Enemy.transform);
        }

        public override void Update()
        {
            base.Update();

            if (this.StateTimer < 0)
                this.StateMachine.ChangeState(this.Enemy.MoveState);
        }


    }
}
