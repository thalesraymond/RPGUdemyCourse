namespace Enemies
{
    public class EnemyGroundedState : EnemyState
    {
        protected EnemyGroundedState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) : base(enemyObject, stateMachine, animationBoolName)
        {
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            
            if (nextState != null) return nextState;

            if (this.EnemyObject.PlayerDetection())
            {
                return this.EnemyObject.BattleState;
            }
            
            return null;
            
        }
    }
}