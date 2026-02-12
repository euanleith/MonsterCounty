using Godot;

namespace MonsterCounty.Actor.Controllers
{
    public partial class Controller : Node
    {
        protected Actor Actor { get; private set; }
        
        public virtual void CustomInit(Actor actor)
        {
            Actor = actor;
        }
        
        public virtual void _Process(double delta)
        {
            // do nothing
        }
    }
}