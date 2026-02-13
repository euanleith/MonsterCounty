using Godot;

namespace MonsterCounty.Actor.Controllers
{
	public partial class MovementController : ActionController<Vector2>
	{
		[Export] public float Speed { get; private set; }

		private Vector2 _prevPosition;

		public override void _PhysicsProcess(double delta)
		{
			if (CurrentAction == null) return;
			_prevPosition = Actor.Position;
			Actor.Position = CurrentAction.Do(delta);
			Actor.Velocity = Vector2.Zero;
			if (_prevPosition != Actor.Position) 
				Actor.Rotation = GetRotation(Actor.Position, _prevPosition);
			Actor.MoveAndSlide();
		}

		private float GetRotation(Vector2 currentPosition, Vector2 prevPosition)
		{
			return (currentPosition - prevPosition).Angle();
		}
	}
}
