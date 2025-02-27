using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Enemy : Entity
    {
        [SerializeField] protected LayerMask WhatIsPlayer;

        [Header("MoveInfo")]
        public float MoveSpeed;
        public float IdleTime;
        public float BattleTime;
        private float defaultMoveSpeed;

        [Header("AttackInfo")]
        public float AttackDistance;
        public float AttackCooldown;
        public float minAttackCooldown;
        public float maxAttackCooldown;
        public float LastTimeAttack;

        [Header("Stunned Info")]
        public float StunDuration;
        public Vector2 StunDirection;
        protected bool CanBeStunned;
        [SerializeField] protected GameObject CounterImage;

        public EnemyStateMachine StateMachine { get; private set; }

        public string LastAnimationName { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            this.StateMachine = new EnemyStateMachine();

            defaultMoveSpeed = MoveSpeed;
        }

        protected override void Update()
        {
            base.Update();

            this.StateMachine.CurrentState.Update();
        }

        public override void SlowEntityBy(float slowPercentage, float slowDurantion)
        {
            this.MoveSpeed = this.MoveSpeed * (1 - slowPercentage);

            base.SlowEntityBy(slowPercentage, slowDurantion);

            Invoke(nameof(ReturnToDefaultSpeed), slowDurantion);
        }

        protected override void ReturnToDefaultSpeed()
        {
            this.MoveSpeed = defaultMoveSpeed;

            base.ReturnToDefaultSpeed();
        }

        public virtual void FreezeTime(bool timeFrozen)
        {
            if (timeFrozen)
            {
                MoveSpeed = 0;
                Anim.speed = 0;
            }
            else
            {
                MoveSpeed = defaultMoveSpeed;
                Anim.speed = 1;
            }
        }

        public virtual IEnumerator FreezeTimeFor(float seconds)
        {
            FreezeTime(true);

            yield return new WaitForSeconds(seconds);

            FreezeTime(false);
        }

        public virtual void StartFreezeTimeForCoroutine(float seconds) => StartCoroutine(FreezeTimeFor(seconds));

        public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(this.wallCheck.position, Vector2.right * FacingDirection, 30, this.WhatIsPlayer);

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, new Vector3(transform.position.x + this.AttackDistance * this.FacingDirection, transform.position.y));
        }

        public virtual void AnimationFinishTrigger() => this.StateMachine.CurrentState.AnimationFinishTrigger();

        public virtual void OpenCounterAttackWindow()
        {
            this.CanBeStunned = true;
            this.CounterImage.SetActive(true);
        }

        public virtual void CloseCounterAttackWindow()
        {
            this.CanBeStunned = false;
            this.CounterImage.SetActive(false);
        }

        public virtual bool CheckCanBeStunned()
        {
            if (!this.CanBeStunned)
                return false;

            this.CloseCounterAttackWindow();

            return true;
        }

        public override void Die()
        {
            base.Die();
            
            Destroy(this.gameObject, 5f);
        }

        public virtual void AssignLastAnimationName(string name) => this.LastAnimationName = name;
    }
}
