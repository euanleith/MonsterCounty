using System;
using System.Linq;
using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Combat.TurnResults;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Combat
{
	public class Combat
	{
		public static Combat Instance { get; private set; }

		public static event Action<CombatActor> ActorDying;
		public static event Action<Party> Exiting;
		
		private readonly Party _playerParty;
		private readonly Party _enemyParty;
		private readonly CircularLinkedList<CombatActor> _turnQueue;

		public Combat(Party playerParty, Party enemyParty)
		{
			if (Instance == null) Instance = this;
			else return;
			_playerParty = playerParty;
			_enemyParty = enemyParty;
			_playerParty.LoadOpponents(_enemyParty);
			_enemyParty.LoadOpponents(_playerParty);
			var shuffled = _playerParty.Concat(_enemyParty).OrderBy(x => new Random().Next()).ToArray();
			_turnQueue = new CircularLinkedList<CombatActor>(shuffled);
			StartTurn();
		}
		
		private void StartTurn()
		{
			CombatActor next = _turnQueue.Peek();
			CombatController combatController = next.CombatController;
			if (combatController.IsAlive)
			{
				TurnResult result = combatController.StartTurn();
				switch (result)
				{
					case ActorToDieResult r:
						if (!OnActorDie(r.Actor)) return;
						break;
					case WaitForInputResult:
						return;
					case RunResult:
						if (!OnActorRun(combatController.Actor)) return;
						break;
				}
			}
			NextTurn();
		}

		public void ResolveTurn(CombatPlayer player, int index)
		{
			CombatController combatController = player.CombatController;
			TurnResult result = combatController.ResolveTurn(index);
			switch (result)
			{
				case ActorToDieResult r:
					if (!OnActorDie(r.Actor)) return;
					break;
				case RunResult:
					if (combatController.Party.GetAliveMembersIndices().Count == 1)
					{
						OnActorRun(combatController.Actor);
						return;
					}
					break;
			}
			NextTurn();
		}

		private void NextTurn()
		{
			_turnQueue.Next();
			StartTurn();
		}
		
		private bool OnActorDie(CombatActor actor)
		{
			GD.Print($"{actor.Name} died!");
			CombatController combatController = actor.CombatController;
			Party party = combatController.Party;
			ActorDying?.Invoke(actor);
			if (party.IsDefeated() || combatController == party.HoldingTheLine)
			{
				if (combatController == party.HoldingTheLine) GD.Print($"{actor.Name} died while holding the line");
				Exit(party);
				return false;
			}
			return true;
		}

		private bool OnActorRun(CombatActor actor)
		{
			CombatController combatController = actor.CombatController;
			if (combatController.Party.HoldingTheLine == combatController)
			{
				GD.Print($"{actor.Name} successfully held the line, party running away!");
				Exit(null);
				return false;
			} 
			GD.Print($"{actor.Name} ran away!");
			return true;
		}

		public void Exit(Party losers)
		{
			GD.Print("exiting combat");
			Instance = null;
			Exiting?.Invoke(losers);
		}
	}
}
