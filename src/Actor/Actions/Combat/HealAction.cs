using System;
using Godot;
using MonsterCounty.Actor.Combat;

namespace MonsterCounty.Actor.Actions.Combat
{
    public partial class HealAction : CombatAction
    {
        [Export] private int _strength = 1;
        
        public override CombatActor Do(double delta)
        {
            GD.Print($"{Actor.Name} healing");
            Self.CurrentHealth = Math.Min(Self.CurrentHealth +  _strength, Self.MaxHealth);
            return base.Do(delta);
        }
    }
}