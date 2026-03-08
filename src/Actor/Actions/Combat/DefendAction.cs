using Godot;
using MonsterCounty.Actor.Combat;

namespace MonsterCounty.Actor.Actions.Combat
{
    [GlobalClass]
    public partial class DefendAction : CombatAction
    {
        public override CombatActor Do(double delta)
        {
            GD.Print($"{Actor.Name} defending");
            Self.IsDefending = true;
            return base.Do(delta);
        }
    }
}