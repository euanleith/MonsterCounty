using System;
using System.Linq;
using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;
using MonsterCounty.State;

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
			CombatController combatController = next.Controllers.Get<CombatController>();
			if (combatController.IsAlive)
			{
				TurnResult res = combatController.StartTurn();
				if (res.WaitingForInput) return;
				if (res.ActorToDie != null)
				{
					if (!OnActorDie(res.ActorToDie)) return;
				}
			}
			NextTurn();
		}

		public void ResolveTurn(CombatPlayer player, int index)
		{
			CombatActor actorToDie = player.Controllers.Get<CombatController>().ResolveTurn(index);
			if (actorToDie != null)
			{
				if (!OnActorDie(actorToDie)) return;
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
			ActorDying?.Invoke(actor);
			Party party = actor.Controllers.Get<CombatController>().Party;
			if (party.IsDefeated())
			{
				Exit(party);
				return false;
			}
			return true;
		}

		private void Exit(Party losers)
		{
			GD.Print("exiting combat");
			Instance = null;
			SaveGameState();
			Exiting?.Invoke(losers);
		}
		
		private void SaveGameState()
		{
			_playerParty.Save();
		}
	}
}
