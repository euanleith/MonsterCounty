using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Combat
{
    [GlobalClass]
    public partial class RunAction : CombatAction
    {
        public override CombatActor Do(double delta)
        {
            GD.Print($"{Actor.Name} holding the line");
            Self.Party.HoldingTheLine = Self;
            foreach (CombatActor opponent in Self.Opponents)
            {
                CombatController combatController = opponent.Controllers.Get<CombatController>();
                if (combatController.IsAlive)
                {
                    GD.Print("setting focus for " + opponent.Name);
                    combatController.Focus = Self;
                }
            }
            return base.Do(delta);
        }
    }
}