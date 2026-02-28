using System.Collections.Generic;
using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Combat;
using MonsterCounty.State;

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

		public void LoadGameState(CombatActorState state)
		{
			CurrentHealth = state.Health;
		}

		public void SaveGameState(List<CombatActorState> state)
		{
			state.Add(new CombatActorState(CurrentHealth));
		}
		
		public CombatActor TakeTurn(double delta)
		{
			return LoadDecision().Choose(Actions).Do(delta);
		}
	}
}
