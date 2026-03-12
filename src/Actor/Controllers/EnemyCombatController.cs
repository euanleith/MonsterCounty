using System;
using System.Collections.Generic;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Combat.TurnResults;

namespace MonsterCounty.Actor.Controllers
{
	public partial class EnemyCombatController : CombatController
	{
		public override TurnResult StartTurn()
		{
			base.StartTurn();
			Random rand = new Random(); // todo choice of target should be in decision too
			List<int> aliveOpponents = Opponents.GetAliveMembersIndices();
			double opponentIndex = aliveOpponents[rand.Next(aliveOpponents.Count)];
			CombatActor actorToDie = TakeTurn(opponentIndex);
			return actorToDie == null ? new PassResult() : new ActorToDieResult(actorToDie);
		}
	}
}
