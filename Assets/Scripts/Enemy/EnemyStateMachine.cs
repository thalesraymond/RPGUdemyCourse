namespace Enemy
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState { get; private set; }

        public void Initialize(EnemyState _startState)
        {
            this.CurrentState = _startState;
            this.CurrentState.Enter();
        }

        public void ChangeState(EnemyState newState)
        {
            this.CurrentState.Exit();

            this.CurrentState = newState;

            this.CurrentState.Enter();
        }
    }
}
