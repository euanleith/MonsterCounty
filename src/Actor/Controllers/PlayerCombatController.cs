using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Combat.TurnResults;
using MonsterCounty.Combat.UI;

namespace MonsterCounty.Actor.Controllers
{
    public partial class PlayerCombatController : CombatController
    {
        public override TurnResult StartTurn()
        {
            base.StartTurn();
            if (Party.HoldingTheLine != null) return new RunResult();
            CombatUi.Instance.Get().NextPlayer(Actor as CombatPlayer);
            return new WaitForInputResult();
        }
		
        public override TurnResult ResolveTurn(int actionIndex, int targetIndex)
        {
            CombatActor actorToDie = Actions[actionIndex].Do(targetIndex);
            if (actorToDie != null) return new ActorToDieResult(actorToDie);
            if (Party.HoldingTheLine == this) return new RunResult();
            return new PassResult();
        }
    }
}