using Godot;

namespace MonsterCounty.Actor.Controllers
{
    public partial class MovementController : ActionController<Vector2>
    {
        [Export] public float Speed { get; private set; }

        public override void _Process(double delta)
        {
            base._Process(delta);
            Actor.Position = currentAction.Do(delta);
        }
    }
}