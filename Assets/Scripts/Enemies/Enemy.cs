using UnityEngine;

namespace Enemies
{
    public class Enemy : Entity
    {
        [Header("Battle Details")]
        public float battleMoveSpeed = 3f;
        public float attackDistance = 2f;
        public float attackHeightTolerance = 1f;
        public float battleTimeDuration = 5f;
        public float minimumRetreatDistance = 1f;
        public Vector2 retreatVelocity;
        public float BattleMoveAnimationMultiplier { get; private set; }
        
        [Header("Enemy Movement Settings")]
        public float idleTime;
        [Range(0,2)]
        public float moveAnimationMultiplier;
        
        [Header("Player Detection")]
        [SerializeField] private LayerMask playerLayer;

        [SerializeField] private Transform playerCheck;
        [SerializeField] private float playerCheckDistance = 10;
        
        [Header("Death Details")]
        public float deathGravityScale = 12f;
        public float deathVelocityY = 15f;
        
        public EnemyHealth HealthComponent { get; private set; }
        
        public EnemyIdleState IdleState { get; private set; }
        public EnemyMoveState MoveState { get; private set; }
        public EnemyAttackState AttackState { get; private set; }
        public EnemyBattleState BattleState { get; private set; }
        public EnemyDeadState DeadState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            this.HealthComponent = this.GetComponent<EnemyHealth>();
            this.IdleState = new EnemyIdleState(this, this.StateMachine, "idle");
            this.MoveState = new EnemyMoveState(this, this.StateMachine, "move");
            this.AttackState = new EnemyAttackState(this, this.StateMachine, "attack");
            this.BattleState = new EnemyBattleState(this, this.StateMachine, "battle");
            this.DeadState = new EnemyDeadState(this, this.StateMachine, "empty");
            
            var speedRatio = this.moveSpeed > 0f
                ? this.battleMoveSpeed / this.moveSpeed
                : 1f;

            this.BattleMoveAnimationMultiplier =
                this.moveAnimationMultiplier * speedRatio;
        }

        protected override void Update()
        {
            base.Update();
        }
        
        // Move enemy towards player when attacked. Useful if a player is attacking from behind
        public void RecordDamage(Transform attacker)
        {
            var attackerDirection = attacker.position - this.transform.position;
            this.HandleRotate(attackerDirection.x);
        }

        public RaycastHit2D PlayerDetection()
        {
            var hit =  Physics2D.Raycast(
                this.playerCheck.position, 
                Vector2.right * this.FacingDirection, 
                this.playerCheckDistance,
                this.playerLayer | this.groundLayer);
            
            if (!hit.collider || hit.collider.gameObject.layer != LayerMask.NameToLayer("player"))
            {
                return default;
            }

            return hit;
        }
        
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = Color.yellow;

            var checkDistance = new Vector3(
                this.playerCheck.position.x + (this.FacingDirection * this.playerCheckDistance),
                this.playerCheck.position.y);
            Gizmos.DrawLine(this.playerCheck.position, checkDistance);
            
            Gizmos.color = Color.blue;
            var attackDistanceVector = new Vector3(
                this.playerCheck.position.x + (this.FacingDirection * this.attackDistance),
                this.playerCheck.position.y);
            Gizmos.DrawLine(this.playerCheck.position, attackDistanceVector);
            
            Gizmos.color = Color.cadetBlue;
            Gizmos.DrawLine(
                attackDistanceVector + Vector3.down * this.attackHeightTolerance,
                attackDistanceVector + Vector3.up * this.attackHeightTolerance);

            Gizmos.color = Color.blueViolet;
            var retreatDistanceVector = new Vector3(
                this.playerCheck.position.x + (this.FacingDirection * this.minimumRetreatDistance),
                this.playerCheck.position.y);
            Gizmos.DrawLine(this.playerCheck.position, retreatDistanceVector);
        }
    }
}
