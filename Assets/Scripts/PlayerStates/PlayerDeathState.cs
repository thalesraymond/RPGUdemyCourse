using GameUI;
using Managers;
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

            this.Player.IsDead = true;
            
            GameObject.Find("Canvas").GetComponent<UI>().SwitchOnEndScreen();
        }

        public override void Update()
        {
            base.Update();

            this.Player.SetVelocityToZero();
        }
    }
}
