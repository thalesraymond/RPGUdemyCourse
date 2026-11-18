using UnityEngine;

namespace Enemies
{
    public class Enemy : Entity
    {
        [Header("Enemy Movement Settings")]
        public float idleTime;
        [Range(0,2)]
        public float moveAnimationMultiplier;
        
        [Header("Player Detection")]
        [SerializeField] private LayerMask playerLayer;

        [SerializeField] private Transform playerCheck;
        [SerializeField] private float playerCheckDistance = 10;
        
        public EnemyIdleState EnemyIdleState { get; private set; }
        public EnemyMoveState EnemyMoveState { get; private set; }
        public EnemyAttackState EnemyAttackState { get; private set; }
        public EnemyBattleState EnemyBattleState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            this.EnemyIdleState = new EnemyIdleState(this, this.StateMachine, "idle");
            this.EnemyMoveState = new EnemyMoveState(this, this.StateMachine, "move");
            this.EnemyAttackState = new EnemyAttackState(this, this.StateMachine, "attack");
            this.EnemyBattleState = new EnemyBattleState(this, this.StateMachine, "battle");
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

            var vectorTo = new Vector3(
                this.playerCheck.position.x + (this.FacingDirection * this.playerCheckDistance),
                this.playerCheck.position.y);
            Gizmos.DrawLine(this.playerCheck.position, vectorTo);
        }
    }
}
