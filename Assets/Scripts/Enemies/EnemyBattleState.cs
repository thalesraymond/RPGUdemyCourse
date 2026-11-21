using UnityEngine;

namespace Enemies
{
    public class EnemyBattleState : EnemyState
    {
        private const float HorizontalDeadzone = 0.1f;

        private Transform _player;
        private float _lastTimePlayerDetected;

        public EnemyBattleState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) 
            : base(enemyObject, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (!this._player)
                this._player = this.EnemyObject.PlayerDetection().transform;
            
            this._lastTimePlayerDetected = Time.time;

            if (this.ShouldRetreat())
                this.RetreatFromPlayer();
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            
            if (nextState != null)
                return nextState;

            if (this.EnemyObject.PlayerDetection()) 
                this._lastTimePlayerDetected = Time.time;
            
            var timeSincePlayerDetected = Time.time - this._lastTimePlayerDetected;
            
            if (timeSincePlayerDetected > this.EnemyObject.battleTimeDuration)
                return this.EnemyObject.IdleState;
            
            if (this.WithinAttackRange() && this.EnemyObject.PlayerDetection())
                return this.EnemyObject.AttackState;
            
            if (!this.EnemyObject.IsGrounded)
                return null;
            
            this.MoveTowardsPlayer();

            return null;
        }

        private void RetreatFromPlayer()
        {
            var directionToPlayer = this.GetDirectionToPlayer();
            var directionToRetreat = directionToPlayer == 0 ? 1 : -directionToPlayer;

            this.RigidBody2D.linearVelocity = new Vector2(
                this.EnemyObject.retreatVelocity.x * directionToRetreat,
                this.EnemyObject.retreatVelocity.y);

            this.EnemyObject.HandleRotate(directionToPlayer);
        }

        private void MoveTowardsPlayer()
        {
            this.EnemyObject.SetVelocity(new Vector2(
                this.EnemyObject.battleMoveSpeed * this.GetDirectionToPlayer(),
                this.RigidBody2D.linearVelocity.y));
        }

        private bool WithinAttackRange() =>
            this.GetDistanceToPlayer() <= this.EnemyObject.attackDistance &&
            this.IsPlayerAtSameHeight();
        
        private bool ShouldRetreat() => this.GetDistanceToPlayer() <= this.EnemyObject.minimumRetreatDistance;

        private bool IsPlayerAtSameHeight()
        {
            if (!this._player)
                return false;

            return Mathf.Abs(this._player.position.y - this.EnemyObject.transform.position.y)
                   <= this.EnemyObject.attackHeightTolerance;
        }

        private float GetDistanceToPlayer()
        {
            return !this._player 
                ? float.MaxValue 
                : Mathf.Abs(this._player.position.x - this.EnemyObject.transform.position.x);
        }

        private int GetDirectionToPlayer()
        {
            if (!this._player)
                return 0;

            var deltaX = this._player.position.x - this.EnemyObject.transform.position.x;

            if (Mathf.Abs(deltaX) <= HorizontalDeadzone)
                return 0;

            return deltaX > 0 ? 1 : -1;
        }
    }
}
