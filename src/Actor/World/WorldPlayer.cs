using System.Collections.Generic;
using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Actor.World
{
	public partial class WorldPlayer : WorldActor
	{
		public static readonly Singleton<WorldPlayer> Instance = new();

		public override void Load()
		{
			if (!Instance.Create(this, false)) return;
			base.Load();
		}

		protected override TypeMap<Controller> LoadControllers()
		{
			TypeMap<Controller> controllers = base.LoadControllers();
			controllers.Add(GetNode<TransmissionController>("TransmissionController"));
			controllers.Add(GetNode<ReceptionController>("ReceptionController"));
			controllers.Add(GetNode<SpawnController>("SpawnController"));
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
