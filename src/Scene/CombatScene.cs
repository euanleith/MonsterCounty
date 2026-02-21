using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Scene
{
	public partial class CombatScene : Scene
	{
		public static readonly Singleton<CombatScene> Instance = new();
		
		[Export] private CombatActor _player;
		[Export] private CombatActor _enemy;

		private CircularLinkedList<CombatActor> _turnOrder;

		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();
			_turnOrder = new CircularLinkedList<CombatActor>(_player, _enemy);
		}

		public void ProcessTurn()
		{
			_turnOrder.Next().Controllers.Get<CombatController>()
				.Attack(_turnOrder.Peek().Controllers.Get<CombatController>());
			if (_turnOrder.Peek().Controllers.Get<CombatController>().CurrentHealth <= 0)
			{
				ChangeToWorldScene();
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
