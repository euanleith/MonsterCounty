using Godot;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Actions.Combat
{
    public partial class AttackAction : CombatAction
    {
        [Export] private int _strength = 2;
        
        public override CombatState Do(double delta)
        {
            GD.Print($"{Actor.Name} hitting opponent");
            Self.Opponent.CurrentHealth -= _strength;
            return base.Do(delta);
        }
    }
}