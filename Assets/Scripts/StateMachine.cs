
public class StateMachine
{
    public EntityState CurrentState { get; private set; }
    
    public void Initialize(EntityState startingState)
    {
        this.CurrentState = startingState;
        this.CurrentState.Enter();
    }
    
    public void ChangeState(EntityState newState)
    {
        this.CurrentState.Exit();
        this.CurrentState = newState;
        this.CurrentState.Enter();
    }

    public void UpdateActiveState()
    {
        this.CurrentState?.Update();
    }
}
