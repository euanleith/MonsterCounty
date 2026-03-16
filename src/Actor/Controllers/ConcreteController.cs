using Godot;

namespace MonsterCounty.Actor.Controllers
{
    public abstract partial class ConcreteController : Node
    {
        public abstract void Load(Actor actor);

        public virtual void Save() { }
    }
}