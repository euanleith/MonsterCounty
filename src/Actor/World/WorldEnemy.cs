using Godot;
using MonsterCounty.Actor.Actions.Movement;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
	public partial class WorldEnemy : WorldActor
	{
		public void CustomInit(World world, PathFollow2D spawnLocation)
		{
			base.CustomInit(world);
			(Controllers.Get<MovementController>().Actions[0] as MobMovementAction).CustomInit(spawnLocation);
		}
	
		private void OnVisibleOnScreenNotifier2DScreenExited()
		{
			QueueFree();
		}
	}
}
