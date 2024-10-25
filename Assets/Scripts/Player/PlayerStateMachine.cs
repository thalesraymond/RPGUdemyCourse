namespace Player
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }

        public void Initialize(PlayerState startState)
        {
            this.CurrentState = startState;

            this.CurrentState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            this.CurrentState.Exit();

            this.CurrentState = newState;

            this.CurrentState.Enter();
        }

    }
}
