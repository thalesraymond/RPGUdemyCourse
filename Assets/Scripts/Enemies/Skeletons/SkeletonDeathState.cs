using UnityEngine;

namespace Enemies.Skeletons
{
    public class SkeletonDeathState : EnemyState
    {
        private EnemySkeleton _enemy;
        public SkeletonDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            this._enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();

            this._enemy.Anim.SetBool(this._enemy.LastAnimationName, true);

            this._enemy.Anim.speed = 0;

            this._enemy.CapsuleCollider.enabled = false;

            this.StateTimer = .15f;
        }

        public override void Update()
        {
            base.Update();

            if (StateTimer > 0)
            {
                Rb.velocity = new Vector2(0, 10);
            }
        }
    }
}
