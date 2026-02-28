using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat.UI;
using MonsterCounty.Model;
using MonsterCounty.State;

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
			_ui.Load(_playerParty, _enemyParty);
			new Combat.Combat(_playerParty, _enemyParty);
			Combat.Combat.Exiting += ChangeToWorldScene;
		}

		public void ChangeToWorldScene()
		{
			ApplyGameState();
			SceneManager.Instance.Get().ChangeScene(GetTree(), SceneManager.CurrentWorldScene);
		}

		private void ApplyGameState()
		{
			GameState.PlayerSpawnName = SpawnController.SpawnType.CurrentPosition.GetStringValue();
		}
		
		public override void _ExitTree()
		{
			Combat.Combat.Exiting -= ChangeToWorldScene;
		}
	}
}
