using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Controllers
{
    public partial class CombatController : ActionController<CustomVoid>
    {
        [Export] private int _maxHealth;
        public int CurrentHealth;

        private int _damage = 1; // todo move to action

        public override void _Ready()
        {
            base._Ready();
            CurrentHealth = _maxHealth; // todo get from state
        }
        
        // todo move to action
        public void Attack(CombatController other)
        {
            GD.Print($"{Actor.Name} hitting {other.Actor.Name}");
            other.GetHit(_damage);
        }

        public void GetHit(int damage)
        {
            CurrentHealth -= damage;
            GD.Print($"{Actor.Name} hit, now at {CurrentHealth} health");
        }
    }
}