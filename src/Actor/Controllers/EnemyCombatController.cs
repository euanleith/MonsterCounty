using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	public partial class EnemyCombatController : CombatController
	{
		protected override Decision<Actor> LoadDecision() => new RandomDecision<Actor>();
	}
}
