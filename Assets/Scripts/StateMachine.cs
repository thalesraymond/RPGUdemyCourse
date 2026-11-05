public class StateMachine
{
    public EntityState CurrentState { get; private set; }
    
    public void Initialize(EntityState startingState)
    {
        this.CurrentState = startingState;
        this.CurrentState.Enter();
    }
    
    private void ChangeState(EntityState newState)
    {
        this.CurrentState.Exit();
        this.CurrentState = newState;
        this.CurrentState.Enter();
    }

    public void RestartCurrentState(EntityState expectedState)
    {
        if (this.CurrentState == expectedState)
            this.ChangeState(expectedState);
    }

    public void UpdateActiveState()
    {
        // States return their first eligible transition; only this module applies it.
        var nextState = this.CurrentState?.Update();
        if (nextState != null)
            this.ChangeState(nextState);
    }
}
