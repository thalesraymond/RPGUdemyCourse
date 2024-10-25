using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerCounterAttackState : PlayerState
    {
        private bool _canCreateClone;
        public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this.StateTimer = Player.CounterAttackDuration;

            Player.Anim.SetBool("SuccessfulCounterAttack", false);

            this._canCreateClone = true;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            this.Player.SetVelocityToZero();

            var colliders = Physics2D.OverlapCircleAll(this.Player.AttackCheck.position, this.Player.AttackCheckRadius)
                .Where(hit => hit.GetComponent<Enemy.Enemy>() is not null);

            foreach (var hit in colliders)
            {
                var canBeStunned = hit.GetComponent<Enemy.Enemy>().CheckCanBeStunned();

                if (canBeStunned)
                {
                    this.StateTimer = 10; // any value bigger than one second
                    Player.Anim.SetBool("SuccessfulCounterAttack", true);

                    this.Player.SkillManager.ParrySkill.UseSkill();

                    if (this._canCreateClone)
                    {
                        this.Player.SkillManager.ParrySkill.MakeMirageOnParry(hit.transform);
                        this._canCreateClone = false;
                    }
                }
            }

            if (this.StateTimer < 0 || this.TriggerCalled)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
            }
        }
    }
}
