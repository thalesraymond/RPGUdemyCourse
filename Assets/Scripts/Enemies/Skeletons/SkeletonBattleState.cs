using UnityEngine;

namespace Enemies.Skeletons
{
    public class SkeletonBattleState : SkeletonGroundedState
    {
        private Transform _player;

        private int _moveDirection;
        public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this._player = PlayerManager.Instance.Player.transform;
        }

        public override void Update()
        {
            base.Update();

            if (this.Enemy.IsPlayerDetected())
            {
                this.StateTimer = this.Enemy.BattleTime;

                if (this.CanAttack())
                    this.StateMachine.ChangeState(this.Enemy.AttackState);
            }
            else if (this.StateTimer < 0 || Vector2.Distance(this._player.transform.position, this.Enemy.transform.position) > 7)
            {
                this.StateMachine.ChangeState(this.Enemy.IdleState);
            }

            this._moveDirection = this.IsPlayerToTheRight() ? 1 : -1;

            this.Enemy.SetVelocity(this._moveDirection * this.Enemy.MoveSpeed, this.Enemy.Rb.velocity.y);
        }

        private bool IsPlayerToTheRight()
        {
            if (this._player.position.x > this.Enemy.transform.position.x)
                return true;
            return false;
        }

        private bool CanAttack()
        {
            if (Time.time >= this.Enemy.LastTimeAttack + this.Enemy.AttackCooldown && this.Enemy.IsPlayerDetected().distance < this.Enemy.AttackDistance)
            {
                this.Enemy.LastTimeAttack = Time.time;

                return true;
            }

            return false;
        }


    }
}
