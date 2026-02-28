using System;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Combat
{
	public partial class CombatEnemy : CombatActor
	{
		public override TurnResult StartTurn()
		{
			Random rand = new Random();
			return new TurnResult(
				false,
				Controllers.Get<CombatController>().TakeTurn(rand.Next(0, Opponents.Count()))
				);
		}
	}
}
