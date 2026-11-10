namespace Enemies
{
    public class EnemyState : EntityState
    {
        protected Enemy Enemy { get; set; }
        
        protected EnemyState(Enemy enemy, StateMachine stateMachine, string animationBoolName) : base(stateMachine, animationBoolName)
        {
            this.Enemy = enemy;
            this.RigidBody2D = enemy.RigidBody2D;
            this.Animator = enemy.Animator;
        }
    }
}