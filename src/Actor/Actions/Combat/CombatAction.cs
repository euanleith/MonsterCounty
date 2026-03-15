using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Combat
{
	[GlobalClass]
	public abstract partial class CombatAction : ControllerAction<CombatActor, CombatActor>
	{
		[Export] public string Name;
		
		protected CombatController Self;

		public override void CustomInit(CombatActor actor)
		{
			base.CustomInit(actor);
			Self = actor.CombatController;
		}
		
		public override CombatActor Do(double delta)
		{
			int index = (int)delta;
			if (Self.CurrentHealth <= 0) return Actor;
			if (Self.Opponents.Get(index).CombatController.CurrentHealth <= 0) return Self.Opponents.Get(index);
			return null;
		}
	}
}
