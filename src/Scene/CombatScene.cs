using System;
using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat;
using MonsterCounty.Combat.UI;
using MonsterCounty.Model;
using MonsterCounty.State;
using MonsterCounty.Utilities;

namespace MonsterCounty.Scene
{
	public partial class CombatScene : Scene
	{
		public static readonly Singleton<CombatScene> Instance = new();

		[Export] private Array<CombatActor> _playerParty;
		[Export] private Array<CombatActor> _enemyParty;
		[Export] private CombatUI _ui;
		
		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();
			Party playerParty = new(_playerParty, GameState.PlayerParty);
			Party enemyParty = new(_enemyParty, GameState.EnemyParty);
			_ui.Load(playerParty, enemyParty);
			new Combat.Combat(playerParty, enemyParty);
			Combat.Combat.Exiting += ChangeToWorldScene;
		}

		public void ChangeToWorldScene(Party losers)
		{
			ApplyGameState(losers);
			SceneManager.Instance.Get().ReturnToWorldScene(GetTree(), SceneManager.CurrentWorldScene);
		}

		private void ApplyGameState(Party losers)
		{
			GameState.PlayerSpawnName = SpawnController.SpawnType.CurrentPosition.GetStringValue();
			if (losers.Any())
			{
				if (losers.Get(0).IsInGroup(Group.ENEMY)) // todo eventually check each individually
				{
					GD.Print("player won!");
					GameState.EntitiesRemoved[Group.ENEMY].Add(GameState.EnemyParty[0].WorldPath);
				}
				else if (losers.Get(0).IsInGroup(Group.PLAYER)) // todo eventually check each individually
				{
					GD.Print("player lost!");
					GameState.PlayerParty.Clear();
				}
			}
		}
		
		public override void _ExitTree()
		{
			Combat.Combat.Exiting -= ChangeToWorldScene;
		}
	}
}
