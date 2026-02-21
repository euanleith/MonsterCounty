using MonsterCounty.Actor.Decisions;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Controllers
{
    public partial class EnemyCombatController : CombatController
    {
        protected override Decision<CombatState> LoadDecision() => new RandomDecision<CombatState>();
    }
}