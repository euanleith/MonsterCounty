using Godot;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Controllers
{
	public partial class CombatController : ActionController<CombatState>
	{
		public CombatController Opponent;
		[Export] private int _maxHealth;
		public int CurrentHealth;
		
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
		
		public void GetHit(int damage)
		{
			CurrentHealth -= damage;
		}
	}
}
