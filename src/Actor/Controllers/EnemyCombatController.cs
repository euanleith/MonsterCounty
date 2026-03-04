using System;
using System.Collections.Generic;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	public partial class EnemyCombatController : CombatController
	{
		protected override Decision<CombatActor> LoadDecision() => new RandomDecision<CombatActor>();
		
		public override TurnResult StartTurn()
		{
			base.StartTurn();
			Random rand = new Random();
			List<int> aliveOpponents = Opponents.GetAliveMembersIndices();
			double opponentIndex = aliveOpponents[rand.Next(aliveOpponents.Count)];
			return new TurnResult(
				false,
				TakeTurn(opponentIndex)
			);
		}
	}
}
