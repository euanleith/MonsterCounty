using MonsterCounty.Actor.Actions.Combat;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Combat;

namespace MonsterCounty.Actor.Decisions.Combat
{
    public class CombatChoice(CombatAction action, CombatPosition combatPosition) : Choice<CombatActor, CombatActor>(action)
    {
        public readonly CombatPosition CombatPosition = combatPosition;
    }
}