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
			CombatActor actorToDie = TakeTurn(-1);
			return actorToDie == null ? new PassResult() : new ActorToDieResult(actorToDie);
		}
	}
}
