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
            SceneManager.Instance.Get().Connect(SceneManager.SignalName.SceneChange, Callable.From(Save)); // using this rather than '+=' syntax as that still runs after the node is freed  
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