using Godot;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Movement
{
	public partial class PathMovementAction : ControllerAction<Vector2>
	{
		private PathFollow2D _pathFollow;

		public override void CustomInit(Actor actor)
		{
			base.CustomInit(actor);
			_pathFollow = Actor.GetParent().GetNode<PathFollow2D>("PathFollow2D");
			if (_pathFollow == null) GD.PushError($"PathMovementAction for {Actor.GetType().Name} missing PathFollow2D sibling.");
		}

		public override Vector2 Do(double delta)
		{
			_pathFollow.ProgressRatio += Actor.Controllers.Get<MovementController>().Speed * (float)delta;
			if (_pathFollow.ProgressRatio > 1f) _pathFollow.ProgressRatio = 0f;
			return _pathFollow.Position;
		}
	}
}
