using Godot;
using MonsterCounty.Actor.Actions.Combat;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat;
using static MonsterCounty.Utilities.EnumUtilities;

namespace MonsterCounty.Actor.Decisions.Combat
{
    [GlobalClass]
    public partial class RandomCombatDecision : RandomDecision<ActionController<CombatActor, CombatActor, CombatAction, double>, CombatActor, CombatActor, CombatAction, double>
    {
        [Export] private bool _changeCombatPosition;
        
        public override CombatChoice Choose(ActionController<CombatActor, CombatActor, CombatAction, double> controller)
        {
            var combatController = controller as CombatController;
            var randAction = base.Choose(controller).Action;
            var randPosition = GetRandomPosition(combatController);
            var randTarget = GetRandomTarget(combatController);
            return new CombatChoice(randAction, randPosition, randTarget);
        }

        private CombatPosition GetRandomPosition(CombatController combatController)
        {
            return _changeCombatPosition ? 
                GetRandomEnumValue<CombatPosition>() : 
                combatController.CombatPosition;
        }

        private int GetRandomTarget(CombatController combatController)
        {
            var aliveOpponents = combatController.Opponents.GetAliveMembersIndices();
            return aliveOpponents[Rand.Next(aliveOpponents.Count)];
        }
    }
}