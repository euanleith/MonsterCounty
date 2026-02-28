using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Combat
{
	public abstract partial class CombatAction : ControllerAction<CombatActor>
	{
		protected CombatController Self;

		public override void CustomInit(Actor actor)
		{
			base.CustomInit(actor);
			Self = actor.Controllers.Get<CombatController>();
		}
		
		public override CombatActor Do(double delta)
		{
			int index = (int)delta;
			CombatActor actor = Actor as CombatActor;
			if (Self.CurrentHealth <= 0) return actor;
			if (actor.Opponents.Get(index).Controllers.Get<CombatController>().CurrentHealth <= 0) return actor.Opponents.Get(index);
			return null;
		}
	}
}
