using Godot;
using MonsterCounty.Scene;

namespace MonsterCounty.Actor.Controllers
{
    public partial class Controller : Node
    {
        protected Actor Actor { get; private set; }
        
        public virtual void Load(Actor actor)
        {
            Actor = actor;
            SceneManager.SceneChanging += Save;
        }

        protected virtual void Save()
        {
            // do nothing
        }

        public override void _Process(double delta)
        {
            // do nothing
        }
    }
}