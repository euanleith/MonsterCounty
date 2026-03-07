using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Combat.UI;

namespace MonsterCounty.Actor.Controllers
{
    public partial class PlayerCombatController : CombatController
    {
        public override TurnResult StartTurn()
        {
            base.StartTurn();
            CombatUI.Instance.Get().NextPlayer(Actor as CombatPlayer);
            return new TurnResult(true);
        }
		
        public override CombatActor ResolveTurn(int index)
        {
            return Actions[index].Do(CombatUI.Instance.Get().SelectedEnemy);
        }
    }
}