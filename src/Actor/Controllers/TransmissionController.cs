using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Controllers
{
    public partial class TransmissionController : ActionController<CustomVoid>
    {
        [Export] public float Range { get; private set; }
        
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            NextAction()?.Do(delta);
        }
    }
}