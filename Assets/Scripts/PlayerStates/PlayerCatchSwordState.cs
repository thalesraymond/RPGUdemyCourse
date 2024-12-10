using UnityEngine;

namespace PlayerStates
{
    public class PlayerCatchSwordState : PlayerState
    {
        private Transform swordTransform;
        public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.Player.PlayerFX.PlayDustFX();
            this.Player.PlayerFX.ScreenShake(this.Player.PlayerFX.shakeSwordImpact);

            this.swordTransform = this.Player.Sword.transform;

            if (Player.transform.position.x > swordTransform.position.x && Player.FacingDirection == 1)
                Player.Flip();
            else if (Player.transform.position.x < swordTransform.position.x && Player.FacingDirection == -1)
                Player.Flip();

            Rb.velocity = new Vector2(Player.SwordReturnImpact * -Player.FacingDirection, Rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();

            Player.StartCoroutine(Player.BusyFor(.1f));
        }

        public override void Update()
        {
            base.Update();

            if (TriggerCalled)
                this.StateMachine.ChangeState(this.Player.IdleState);
        }
    }
}
