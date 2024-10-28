using GameUI;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerDeathState : PlayerState
    {
        public PlayerDeathState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            GameObject.Find("Canvas").GetComponent<UI>().SwitchOnEndScreen();
        }

        public override void Update()
        {
            base.Update();

            this.Player.SetVelocityToZero();
        }
    }
}
