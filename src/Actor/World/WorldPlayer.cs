using System.Collections.Generic;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Actor.World
{
	public partial class WorldPlayer : WorldActor
	{
		public static readonly Singleton<WorldPlayer> Instance = new();
		
		public TransmissionController TransmissionController;
		public SpawnController SpawnController;

		public override void Load()
		{
			if (!Instance.Create(this, false)) return;
			base.Load();
		}

		protected override TypeMap<ConcreteController> LoadControllers()
		{
			TypeMap<ConcreteController> controllers = base.LoadControllers();
			TransmissionController = GetNode<TransmissionController>("TransmissionController");
			SpawnController = GetNode<SpawnController>("SpawnController");
			controllers.Add(TransmissionController);
			controllers.Add(SpawnController);
			return controllers;
		}
		
		protected override void LoadParty()
		{
			List<CombatActorState> partyState  = GetPartyState();
			if (partyState.Count != 0) Party.LoadFromGameState(partyState);
			else Party.Load(partyState);
		}
		
		protected override List<CombatActorState> GetPartyState() => GameState.PlayerParty;
	}
}
