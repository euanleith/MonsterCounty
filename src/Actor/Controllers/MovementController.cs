using Godot;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.World;
using MonsterCounty.State;
using static MonsterCounty.Utilities.VectorUtilities;

namespace MonsterCounty.Actor.Controllers
{
	public partial class MovementController : ActionController<Vector2>
	{
		[Export] public float Speed { get; private set; }

		public bool IsMoving { get; private set; }
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
			{
				IsMoving = true; 
				Actor.Controllers.Get<VisualController>().UpdateAnimation(GetDirection(Actor.Position, _prevPosition));
				Actor.Rotation = GetRotation(Actor.Position, _prevPosition);
			}
			else IsMoving = false;

			Actor.MoveAndSlide();
		}
	}
}
