using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Combat
{
    public partial class AttackAction : CombatAction
    {
        [Export] private int _strength = 2;
        
        public override CombatActor Do(double delta)
        {
            int index = (int)delta;
            CombatActor actor = Actor as CombatActor;
            GD.Print($"{Actor.Name} hitting {actor.Opponents.Get(index).Name}");
            actor.Opponents.Get(index).Controllers.Get<CombatController>().CurrentHealth -= _strength;
            return base.Do(delta);
        }
    }
}