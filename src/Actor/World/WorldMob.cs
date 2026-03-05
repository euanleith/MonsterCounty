using Godot;
using MonsterCounty.Actor.Actions.Movement;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
	public partial class WorldMob : WorldEnemy
	{
		public void Start(PathFollow2D spawnLocation)
		{
			(Controllers.Get<MovementController>().Actions[0] as MobMovementAction).CustomInit(spawnLocation);
		}
	
		private void OnVisibleOnScreenNotifier2DScreenExited()
		{
			QueueFree();
		}
	}
}
