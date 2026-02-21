using Godot;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.World;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Controllers
{
	public partial class MovementController : ActionController<Vector2>
	{
		[Export] public float Speed { get; private set; }

		private Vector2 _prevPosition;

		protected override void Save()
		{
			base.Save();
			GameState.PlayerPosition = WorldPlayer.Instance.Get().GlobalPosition;
		}

		public override void _PhysicsProcess(double delta)
		{
			ControllerAction<Vector2> currentAction = NextAction();
			if (currentAction == null) return;
			_prevPosition = Actor.Position;
			Actor.Position = currentAction.Do(delta);
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
