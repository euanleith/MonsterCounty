using System.Collections.Generic;
using MonsterCounty.State;

namespace MonsterCounty.Actor.World
{
	public partial class WorldNPC : WorldActor
	{
		protected override List<CombatActorState> GetPartyState() => [];
	}
}
