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
			playerCombatController.LoadOpponent(enemyCombatController);
			enemyCombatController.LoadOpponent(playerCombatController);
		}

		public void ProcessTurn(ControllerAction<CombatState> playerAction)
		{
			double delta = -1;
			// todo clean up
			CombatState state = playerAction.Do(delta);
			if (state == CombatState.Win)
			{
				GD.Print("player won!");
				ChangeToWorldScene();
				return;
			}
			if (state == CombatState.Lose)
			{
				GD.Print("player lost :(");
				ChangeToWorldScene();
				return;
			}
			state = _enemy.Controllers.Get<CombatController>().TakeTurn(delta);
			if (state == CombatState.Win)
			{
				GD.Print("player lost :(");
				ChangeToWorldScene();
				return;
			}
			if (state == CombatState.Lose)
			{
				GD.Print("player won!");
				ChangeToWorldScene();
				return;
			}
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
