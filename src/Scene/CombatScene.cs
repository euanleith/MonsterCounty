using System;
using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Combat;
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
		
		private CircularLinkedList<CombatActor> _turnQueue;
		private readonly Random _rand = new();

		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();
			_ui.Load(_playerParty, _enemyParty);
			LoadOpponents(_playerParty, _enemyParty);
			LoadOpponents(_enemyParty, _playerParty);
			var shuffled = _playerParty.Concat(_enemyParty).OrderBy(x => _rand.Next()).ToArray();
			_turnQueue = new CircularLinkedList<CombatActor>(shuffled);
			StartTurn();
		}
		
		private void LoadOpponents(Array<CombatActor> self, Array<CombatActor> opponents)
		{
			foreach (CombatActor member in self)
			{
				member.Controllers.Get<CombatController>().LoadOpponents(opponents);
			}
		}

		private void ProcessTurn(CombatActor actorToDie)
		{
			if (actorToDie != null)
			{
				if (!OnActorDie(actorToDie)) return;
			}
			_turnQueue.Next();
			StartTurn();
		}

		public void ProcessPlayerTurn(ControllerAction<CombatActor> playerAction)
		{
			ProcessTurn(playerAction.Do(CombatUI.Instance.Get().SelectedEnemy));
		}

		private void ProcessEnemyTurn()
		{
			ProcessTurn(_turnQueue.Peek().Controllers.Get<CombatController>().TakeTurn(_rand.Next(0, _playerParty.Count)));
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
			actor.Visible = false;
			if (actor.IsInGroup("enemy"))
			{
				_enemyParty.Remove(actor);
			}
			else if (actor.IsInGroup("player"))
			{
				_playerParty.Remove(actor);
			}
			CombatUI.Instance.Get().Reset();
		}

		private void ChangeToWorldScene()
		{
			GameState.PlayerSpawnName = SpawnController.SpawnType.CurrentPosition.GetStringValue();
			SceneManager.Instance.Get().ChangeScene(GetTree(), SceneManager.CurrentWorldScene);
		}
		
		private void StartTurn()
		{
			if (_turnQueue.Peek().IsInGroup("enemy"))
			{
				ProcessEnemyTurn();
			}
			else if (_turnQueue.Peek().IsInGroup("player"))
			{
				CombatUI.Instance.Get().NextPlayer(_turnQueue.Peek());
			}
		}
	}
}
