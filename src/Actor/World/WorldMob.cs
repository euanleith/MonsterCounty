using Godot;
using MonsterCounty.Actor.Actions.Movement;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
	public partial class WorldMob : WorldActor
	{
		public void Start(PathFollow2D spawnLocation)
		{
			(Controllers.Get<MovementController>().Actions[0] as MobMovementAction).CustomInit(spawnLocation);
		}
		
		protected override TypeMap<Controller> LoadControllers()
		{
			TypeMap<Controller> controllers = base.LoadControllers();
			controllers.Add(GetNode<TransmissionController>("TransmissionController"));
			return controllers;
		}
	
		private void OnVisibleOnScreenNotifier2DScreenExited()
		{
			QueueFree();
		}
	}
}
