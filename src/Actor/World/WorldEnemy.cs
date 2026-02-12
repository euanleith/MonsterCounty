using Godot;
using MonsterCounty.Actor.Actions.Movement;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.World
{
	public partial class WorldEnemy : WorldActor
	{
		public void CustomInit(World world, PathFollow2D spawnLocation)
		{
			base.CustomInit(world);
			// Controllers.Get<MovementController>().Actions[0] = 
			// 	Controllers.Get<MovementController>().Actions[0].Duplicate() as EnemyMovementAction;
			(Controllers.Get<MovementController>().Actions[0] as EnemyMovementAction).CustomInit(spawnLocation);
		}
		
		public override void _Ready()
		{
			base._Ready();
			var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
			animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
		}
	
		private void OnVisibleOnScreenNotifier2DScreenExited()
		{
			QueueFree();
		}
	}
}
