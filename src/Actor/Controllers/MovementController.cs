using Godot;

namespace MonsterCounty.Actor.Controllers
{
	public partial class MovementController : ActionController<Vector2>
	{
		// todo integrate with Path's
		[Export] public float Speed { get; private set; }

		public override void _PhysicsProcess(double delta)
		{
			if (currentAction == null) return;
			Actor.Velocity = currentAction.Do(delta);
			Actor.MoveAndSlide();
		}
	}
}
