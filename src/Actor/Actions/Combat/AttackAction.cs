using Godot;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Combat
{
    public partial class AttackAction : CombatAction
    {
        [Export] private int _strength = 2;
        
        public override Actor Do(double delta)
        {
            GD.Print($"{Actor.Name} hitting {Self.Opponent.Name}");
            Self.Opponent.Controllers.Get<CombatController>().CurrentHealth -= _strength;
            return base.Do(delta);
        }
    }
}