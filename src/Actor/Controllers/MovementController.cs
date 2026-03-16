using Godot;
using MonsterCounty.Actor.World;
using MonsterCounty.State;
using static MonsterCounty.Utilities.VectorUtilities;

namespace MonsterCounty.Actor.Controllers
{
	public partial class MovementController : InstancedActionController<Vector2, WorldActor>
	{
		[Export] public float Speed { get; private set; }
		[Export] public float RunSpeed { get; private set; }

		public bool IsMoving { get; private set; }
		private Vector2 _prevPosition;
		public bool IsRunning;

		public override void Save()
		{
			base.Save();
			GameState.PlayerPosition = WorldPlayer.Instance.Get().GlobalPosition;
		}

		public override void _PhysicsProcess(double delta)
		{
			var currentAction = NextAction();
			if (currentAction == null) return;
			_prevPosition = Actor.Position;
			Actor.Position = currentAction.Do(delta);
			Actor.Velocity = Vector2.Zero;
			if (_prevPosition != Actor.Position)
			{
				IsMoving = true; 
				Actor.VisualController.UpdateAnimation(GetDirection(Actor.Position, _prevPosition), IsRunning);
				Actor.Rotation = GetRotation(Actor.Position, _prevPosition);
			}
			else IsMoving = false;

			Actor.MoveAndSlide();
		}
	}
}
