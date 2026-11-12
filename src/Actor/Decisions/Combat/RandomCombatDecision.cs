using Godot;
using MonsterCounty.Actor.Actions.Combat;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions.Combat
{
    [GlobalClass]
    public partial class RandomCombatDecision : RandomDecision<ActionController<CombatActor, CombatActor, CombatAction, double>, CombatActor, CombatActor, CombatAction, double>
    {
        public override CombatChoice Choose(ActionController<CombatActor, CombatActor, CombatAction, double> controller)
        {
            var combatController = controller as CombatController;
            var randAction = base.Choose(controller).Action;
            var combatPosition = combatController.CombatPosition;
            var randTarget = GetRandomTarget(combatController);
            return new CombatChoice(randAction, combatPosition, randTarget);
        }
        
        private int GetRandomTarget(CombatController combatController)
        {
            var aliveOpponents = combatController.Opponents.GetAliveMembersIndices();
            return aliveOpponents[Rand.Next(aliveOpponents.Count)];
        }
    }
}