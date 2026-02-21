using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Actions.Combat
{
	public abstract partial class CombatAction : ControllerAction<CombatState>
	{
		protected CombatController Self;

		public override void CustomInit(Actor actor)
		{
			base.CustomInit(actor);
			Self = actor.Controllers.Get<CombatController>();
		}
		
		public override CombatState Do(double delta)
		{
			if (Self.CurrentHealth <= 0) return CombatState.Lose;
			if (Self.Opponent.CurrentHealth <= 0) return CombatState.Win;
			return CombatState.Continue;
		}
	}
}
