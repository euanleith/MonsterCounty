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
			CombatController player = CombatPlayer.Instance.Get().Controllers.Get<CombatController>();
			CombatController enemy = CombatEnemy.Instance.Get().Controllers.Get<CombatController>();
			BindHealthLabel(player, _playerHealthLabel, "Player");
			BindHealthLabel(enemy, _enemyHealthLabel, "Enemy");
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
		
		private void BindHealthLabel(CombatController controller, Label label, string actorName)
		{
			controller.CurrentHealthChanged += health =>
				OnHealthChanged(health, label, actorName);
			OnHealthChanged(controller.CurrentHealth, label, actorName);
		}

		private static void OnHealthChanged(int health, Label label, string actorName)
		{
			label.Text = $"{actorName} health: {health}";
		}
	}
}
