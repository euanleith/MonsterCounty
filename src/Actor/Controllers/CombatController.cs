using Godot;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Controllers
{
	public partial class CombatController : ActionController<CombatState>
	{
		[Export] private int _maxHealth;
		
		[Signal] public delegate void CurrentHealthChangedEventHandler(int health);
		
		public CombatController Opponent;
		private int _currentHealth;
		public int CurrentHealth
		{
			get => _currentHealth;
			set { EmitSignal(nameof(CurrentHealthChanged), value); _currentHealth = value; }
		}
		
		public override void Load(Actor actor)
		{
			base.Load(actor);
			CurrentHealth = _maxHealth; // todo get from state
		}

		public void LoadOpponent(CombatController other)
		{
			Opponent = other;
		}
		
		public CombatState TakeTurn(double delta)
		{
			return LoadDecision().Choose(Actions).Do(delta);
		}
	}
}
