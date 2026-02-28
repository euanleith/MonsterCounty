using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Combat.Parties;

namespace MonsterCounty.Actor.Controllers
{
	public partial class CombatController : ActionController<CombatActor>
	{
		[Export] public int MaxHealth;
		
		[Signal] public delegate void CurrentHealthChangedEventHandler(int health);

		public Party Opponents;
		private int _currentHealth;
		public int CurrentHealth
		{
			get => _currentHealth;
			set { EmitSignal(nameof(CurrentHealthChanged), value); _currentHealth = value; }
		}
		
		public override void Load(Actor actor)
		{
			base.Load(actor);
			CurrentHealth = MaxHealth; // todo get from state
		}
		
		public CombatActor TakeTurn(double delta)
		{
			return LoadDecision().Choose(Actions).Do(delta);
		}
	}
}
