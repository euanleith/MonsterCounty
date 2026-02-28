using System;
using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Combat.Parties;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;
using MonsterCounty.UI;

namespace MonsterCounty.Scene
{
	public partial class CombatScene : Scene
	{
		public static readonly Singleton<CombatScene> Instance = new();

		[Export] private Array<CombatActor> _playerParty;
		[Export] private Array<CombatActor> _enemyParty;
		[Export] private CombatUI _ui;
		
		private Party _playerPartyInternal;
		private Party _enemyPartyInternal;
		private CircularLinkedList<CombatActor> _turnQueue;
		private readonly Random _rand = new();

		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();
			_playerPartyInternal = new(_playerParty);
			_enemyPartyInternal = new(_enemyParty);
			_playerPartyInternal.LoadOpponents(_enemyPartyInternal);
			_enemyPartyInternal.LoadOpponents(_playerPartyInternal);
			_ui.Load(_playerPartyInternal, _enemyPartyInternal);
			var shuffled = _playerPartyInternal.Concat(_enemyPartyInternal).OrderBy(x => _rand.Next()).ToArray();
			_turnQueue = new CircularLinkedList<CombatActor>(shuffled);
			StartTurn();
		}

		private bool OnActorDie(CombatActor actor)
		{
			GD.Print($"{actor.Name} died!");
			RemoveActor(actor);
			if (!_turnQueue.Contains(actor.GetType()))
			{
				GD.Print("exiting combat");
				ChangeToWorldScene();
				return false;
			}
			return true;
		}

		private void RemoveActor(CombatActor actor)
		{
			_turnQueue.Remove(actor);
			actor.Party.Remove(actor);
			CombatUI.Instance.Get().RemoveActor(actor);
		}

		private void ChangeToWorldScene()
		{
			GameState.PlayerSpawnName = SpawnController.SpawnType.CurrentPosition.GetStringValue();
			SceneManager.Instance.Get().ChangeScene(GetTree(), SceneManager.CurrentWorldScene);
		}
		
		private void StartTurn()
		{
			TurnResult res = _turnQueue.Peek().StartTurn();
			if (res.WaitingForInput) return;
			if (res.ActorToDie != null)
			{
				if (!OnActorDie(res.ActorToDie)) return;
			}
			NextTurn();
		}

		public void ResolveTurn(CombatPlayer player, int index)
		{
			CombatActor actorToDie = player.ResolveTurn(index);
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
	}
}
