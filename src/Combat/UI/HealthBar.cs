using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Combat.UI
{
	public partial class HealthBar : Container
	{
		[Export] private Label _name;
		[Export] private ProgressBar _bar;
		[Export] private Label _health;

		public void Bind(CombatActor actor)
		{
			CombatController combatController = actor.CombatController;
			if (!combatController.IsAlive)
			{
				Visible = false;
				return;
			}
			_name.Text = actor.Name;
			_bar.MaxValue = combatController.MaxHealth;
			OnHealthChanged(combatController.CurrentHealth);
			combatController.CurrentHealthChanged += OnHealthChanged;
		}

		private void OnHealthChanged(int hp)
		{
			_bar.Value = hp;
			_health.Text = hp.ToString();
		}
	}
}
