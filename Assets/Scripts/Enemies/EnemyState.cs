using UnityEngine;

namespace Enemies
{
    public class EnemyState
    {
        protected EnemyStateMachine StateMachine;

        protected Enemy EnemyBase;

        protected bool triggerCalled;

        public Rigidbody2D Rb { get; private set; }

        private string animBoolName;

        protected float StateTimer;

        public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        {
            this.EnemyBase = enemyBase;
            this.StateMachine = stateMachine;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            triggerCalled = false;
            this.Rb = this.EnemyBase.Rb;
            this.EnemyBase.Anim.SetBool(this.animBoolName, true);
        }

        public virtual void Exit()
        {
            this.EnemyBase.Anim.SetBool(this.animBoolName, false);

            this.EnemyBase.AssignLastAnimationName(this.animBoolName);
        }

        public virtual void Update()
        {
            this.StateTimer -= Time.deltaTime;

        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }

    }
}
