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
            GD.Print($"{Actor.Name} attacking {Self.Opponents.Get(index).Name}");
            if (RollToHit(opponent)) opponent.CurrentHealth -= RollDamage();
            return base.Do(delta);
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