using MonsterCounty.Actor.Actions.Combat;
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
            CombatUI.Instance.Get().NextPlayer(Actor as CombatPlayer);
            return new WaitForInputResult();
        }
		
        public override TurnResult ResolveTurn(int index)
        {
            CombatActor actorToDie = Actions[index].Do(CombatUI.Instance.Get().SelectedEnemy);
            if (actorToDie != null) return new ActorToDieResult(actorToDie);
            if (Party.HoldingTheLine == this) return new RunResult();
            return new PassResult();
        }
    }
}