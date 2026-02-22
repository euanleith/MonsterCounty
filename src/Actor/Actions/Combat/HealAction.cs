using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Actions.Combat
{
    public partial class HealAction : CombatAction
    {
        [Export] private int _strength = 1;
        
        public override Actor Do(double delta)
        {
            GD.Print($"{Actor.Name} healing");
            Self.CurrentHealth += _strength;
            return base.Do(delta);
        }
    }
}