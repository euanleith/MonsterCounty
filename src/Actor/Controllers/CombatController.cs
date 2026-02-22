using Godot;

namespace MonsterCounty.Actor.Controllers
{
	public partial class CombatController : ActionController<Actor>
	{
		[Export] private int _maxHealth;
		
		[Signal] public delegate void CurrentHealthChangedEventHandler(int health);
		
		public Actor Opponent;
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

		public void LoadOpponent(Actor other)
		{
			Opponent = other;
		}
		
		public Actor TakeTurn(double delta)
		{
			return LoadDecision().Choose(Actions).Do(delta);
		}
	}
}
