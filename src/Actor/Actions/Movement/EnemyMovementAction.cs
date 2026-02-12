using Godot;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Movement
{
	[Tool]
	public partial class EnemyMovementAction : ControllerAction<Vector2>
	{
		public void CustomInit(PathFollow2D spawnLocation)
		{
			base.CustomInit(Actor);
			
			spawnLocation.ProgressRatio = GD.Randf();
			float direction = spawnLocation.Rotation + Mathf.Pi / 2;

			Actor.Position = spawnLocation.Position;

			direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
			Actor.Rotation = direction;

			var velocity = new Vector2(Actor.Controllers.Get<MovementController>().Speed, 0);
			Actor.Velocity = velocity.Rotated(direction);
		}
		
		public override Vector2 Do(double delta)
		{
			return Actor.Velocity;
		}
	}
}
