using System;
using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Actor.World;

namespace MonsterCounty.Actor.Actions.Movement
{
	[GlobalClass]
	public partial class PathMovementAction : ControllerAction<Vector2, WorldActor>
	{
		private Path2D _path;
		private PathFollow2D _pathFollow;

		public override void CustomInit(WorldActor actor)
		{
			base.CustomInit(actor);
			try
			{
				_path = Actor.MovementController.GetNode<Path2D>("Path2D");
				_path.Owner = Actor;
				_pathFollow = _path.GetNode<PathFollow2D>("PathFollow2D");
			}
			catch (Exception)
			{
				GD.PushError($"MovementController with PathMovementAction for {Actor.GetType().Name} missing Path2D/PathFollow2D child.");
			}
		}

		public override Vector2 Do(double delta)
		{
			_pathFollow.Progress = Mathf.Wrap(
				_pathFollow.Progress + Actor.MovementController.Speed * (float)delta,
				0f,
				_path.Curve.GetBakedLength()
			);
			return _pathFollow.Position;
		}
	}
}
