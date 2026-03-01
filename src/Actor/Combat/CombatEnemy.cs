using System;
using System.Collections.Generic;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Combat
{
	public partial class CombatEnemy : CombatActor
	{
		public override TurnResult StartTurn()
		{
			Random rand = new Random();
			List<int> aliveOpponents = Opponents.GetAliveMembersIndices();
			double opponentIndex = aliveOpponents[rand.Next(aliveOpponents.Count)];
			return new TurnResult(
				false,
				Controllers.Get<CombatController>().TakeTurn(opponentIndex)
				);
		}
	}
}
