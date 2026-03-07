using Godot;
using MonsterCounty.Actor.Actions.Combat;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat;
using static MonsterCounty.Utilities.EnumUtilities;

namespace MonsterCounty.Actor.Decisions.Combat
{
    public partial class RandomCombatDecision : RandomDecision<ActionController<CombatActor>, CombatActor>
    {
        public override CombatChoice Choose(ActionController<CombatActor> controller)
        {
            var randomAction = base.Choose(controller).Action;
            var position = GetRandomEnumValue<CombatPosition>();
            return new CombatChoice(randomAction as CombatAction, position);
        }
    }
}