using System.Collections.Generic;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Actor.World
{
	public partial class WorldEnemy : WorldActor
	{
		public TransmissionController TransmissionController;
		
		protected override TypeMap<ConcreteController> LoadControllers()
		{
			TypeMap<ConcreteController> controllers = base.LoadControllers();
			TransmissionController = GetNode<TransmissionController>("TransmissionController");
			controllers.Add(TransmissionController);
			return controllers;
		}
		
		protected override List<CombatActorState> GetPartyState() => GameState.EnemyParty;
	}
}
