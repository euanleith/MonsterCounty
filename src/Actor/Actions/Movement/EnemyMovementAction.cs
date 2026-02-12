using Godot;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Movement
{
	[Tool]
	// todo rename like RuntimeEnemy or SpawningEnemy or something idk
	public partial class EnemyMovementAction : ControllerAction<Vector2>
	{
		private Vector2 _velocity;
		public void CustomInit(PathFollow2D spawnLocation)
		{
			base.CustomInit(Actor);
			
			spawnLocation.ProgressRatio = GD.Randf();
			float direction = spawnLocation.Rotation + Mathf.Pi / 2;

			Actor.Position = spawnLocation.Position;

			direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
			var velocity = new Vector2(Actor.Controllers.Get<MovementController>().Speed, 0);
			_velocity = velocity.Rotated(direction);
		}
		
		public override Vector2 Do(double delta)
		{
			return Actor.Position + _velocity * (float)delta;
		}
	}
}
