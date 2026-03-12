using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Utilities;

namespace MonsterCounty.Actor.Actions.Combat
{
    [GlobalClass]
    public partial class AttackAction : CombatAction
    {
        public override CombatActor Do(double delta)
        {
            int index = (int)delta;
            CombatController opponent = Self.Opponents.Get(index).Controllers.Get<CombatController>();
            if (Self.Focus != null)
            {
                GD.Print($"{Actor.Name} focused on {Self.Focus.Actor.Name}");
                opponent = Self.Focus;
                index = Self.Opponents.IndexOf(opponent.Actor as CombatActor);
            }
            GD.Print($"{Actor.Name} attacking {opponent.Actor.Name}");
            if (RollToHit(opponent)) opponent.CurrentHealth -= RollDamage();
            return base.Do(index);
        }

        private bool RollToHit(CombatController opponent)
        {
            int toHit = Dice.Roll(2, 6);
            int toDefend = opponent.RollToDefend();
            GD.Print($"{toHit} to hit, {toDefend} to defend");
            return toHit >= toDefend;
        }

        private int RollDamage()
        {
            int damage = Self.Weapon.Dice.Roll();
            GD.Print($"{damage} damage");
            return damage;
        }
    }
}