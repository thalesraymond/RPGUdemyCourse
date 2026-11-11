namespace Enemies
{
    public class EnemyState : EntityState
    {
        protected Enemy EnemyObject { get; set; }
        
        protected EnemyState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) : base(stateMachine, animationBoolName)
        {
            this.EnemyObject = enemyObject;
            this.RigidBody2D = enemyObject.RigidBody2D;
            this.Animator = enemyObject.Animator;
        }
    }
}