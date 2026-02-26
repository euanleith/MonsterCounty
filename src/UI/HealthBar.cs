using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.UI
{
	public partial class HealthBar : Control
	{
		[Export] private Label _name;
		[Export] private ProgressBar _bar;
		[Export] private Label _health;

		public void Bind(CombatActor actor)
		{
			_name.Text = actor.Name;
			CombatController combatController = actor.Controllers.Get<CombatController>();
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
