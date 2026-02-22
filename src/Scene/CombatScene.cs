using System;
using Godot;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Scene
{
	public partial class CombatScene : Scene
	{
		public static readonly Singleton<CombatScene> Instance = new();
		
		// todo these are singletons now
		[Export] private CombatActor _player;
		[Export] private CombatActor _enemy;

		private CircularLinkedList<CombatActor> _turnOrder;

		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();
			_turnOrder = new CircularLinkedList<CombatActor>(_player, _enemy);
			CombatController playerCombatController = _player.Controllers.Get<CombatController>();
			CombatController enemyCombatController = _enemy.Controllers.Get<CombatController>();
			playerCombatController.LoadOpponent(_enemy);
			enemyCombatController.LoadOpponent(_player);
		}

		public void ProcessTurn(ControllerAction<Actor.Actor> playerAction)
		{
			double delta = -1;
			Actor.Actor actorToDie = playerAction.Do(delta);
			if (actorToDie != null)
			{
				OnActorDie(actorToDie);
				return;
			}
			actorToDie = _enemy.Controllers.Get<CombatController>().TakeTurn(delta);
			if (actorToDie != null)
			{
				OnActorDie(actorToDie);
				return;
			}
		}

		private void OnActorDie(Actor.Actor actor)
		{
			GD.Print($"{actor.Name} died!");
			ChangeToWorldScene();
		}

		private void ChangeToWorldScene()
		{
			GameState.PlayerSpawnName = SpawnController.SpawnType.CurrentPosition.GetStringValue();
			SceneManager.Instance.Get().ChangeScene(GetTree(), SceneManager.CurrentWorldScene);
		}
		
		private void NextTurn()
		{
			_turnOrder.Next();
		}
	}
}
