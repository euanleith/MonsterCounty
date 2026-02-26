using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	public partial class EnemyCombatController : CombatController
	{
		protected override Decision<CombatActor> LoadDecision() => new RandomDecision<CombatActor>();
	}
}
