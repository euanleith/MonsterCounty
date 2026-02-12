using Godot;

namespace MonsterCounty.Actor.Controllers
{
    public partial class MovementController : ActionController<Vector2>
    {
        [Export] public float Speed { get; private set; }

        public override void _PhysicsProcess(double delta)
        {
            base._Process(delta);
            Actor.Velocity = currentAction.Do(delta);
            Actor.MoveAndSlide();
        }
    }
}