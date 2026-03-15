using Godot;
using MonsterCounty.Actor.Actions.Combat;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat;
using static MonsterCounty.Utilities.EnumUtilities;

namespace MonsterCounty.Actor.Decisions.Combat
{
    [GlobalClass]
    public partial class RandomCombatDecision : RandomDecision<ActionController<CombatActor, CombatActor>, CombatActor, CombatActor>
    {
        [Export] private bool _changeCombatPosition;
        
        public override CombatChoice Choose(ActionController<CombatActor, CombatActor> controller)
        { 
            var randomAction = base.Choose(controller).Action;
            var position = _changeCombatPosition ? 
                GetRandomEnumValue<CombatPosition>() : 
                (controller as CombatController).CombatPosition;
            return new CombatChoice(randomAction as CombatAction, position);
        }
    }
}