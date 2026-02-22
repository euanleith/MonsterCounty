using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Combat
{
	public abstract partial class CombatAction : ControllerAction<Actor>
	{
		protected CombatController Self;

		public override void CustomInit(Actor actor)
		{
			base.CustomInit(actor);
			Self = actor.Controllers.Get<CombatController>();
		}
		
		public override Actor Do(double delta)
		{
			if (Self.CurrentHealth <= 0) return Actor;
			if (Self.Opponent.Controllers.Get<CombatController>().CurrentHealth <= 0) return Self.Opponent;
			return null;
		}
	}
}
