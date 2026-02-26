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
            GD.Print($"{Actor.Name} hitting {Self.Opponents[index].Name}");
            Self.Opponents[index].Controllers.Get<CombatController>().CurrentHealth -= _strength;
            return base.Do(delta);
        }
    }
}