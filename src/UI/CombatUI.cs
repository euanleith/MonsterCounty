using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.Scene;

namespace MonsterCounty.UI
{
	public partial class CombatUI : CanvasLayer, Loadable
	{
		private static readonly Singleton<CombatUI> Instance = new();

		[Export] private Array<Button> _buttons;
		[Export] private Label _playerHealthLabel;
		[Export] private Label _enemyHealthLabel;

		public void Load()
		{
			if (!Instance.Create(this, false)) return;
			
			InitCombatButtons();
			// todo generalise
			CombatController player = CombatPlayer.Instance.Get().Controllers.Get<CombatController>();
			player.Connect(nameof(CombatController.CurrentHealthChanged), 
				new Callable(this, nameof(SetPlayerHealthLabel)));
			CombatController enemy = CombatEnemy.Instance.Get().Controllers.Get<CombatController>();
			enemy.Connect(nameof(CombatController.CurrentHealthChanged), 
				new Callable(this, nameof(SetEnemyHealthLabel)));
			SetPlayerHealthLabel(player.CurrentHealth);
			SetEnemyHealthLabel(enemy.CurrentHealth);
		}

		public void SetPlayerHealthLabel(int health)
		{
			_playerHealthLabel.Text = $"Player health: {health}";
		}

		public void SetEnemyHealthLabel(int health)
		{
			_enemyHealthLabel.Text = $"Enemy health: {health}";
		}
		
		private void InitCombatButtons()
		{
			CombatController player = CombatPlayer.Instance.Get().Controllers.Get<CombatController>();
			for (int i = 0; i < _buttons.Count; i++)
			{
				_buttons[i].Text = player.Actions[i].Name;
				int capturedIndex = i;
				_buttons[i].Pressed += () => CombatScene.Instance.Get().ProcessTurn(player.Actions[capturedIndex]);
			}
		}
	}
}
