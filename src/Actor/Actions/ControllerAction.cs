using Godot;

namespace MonsterCounty.Actor.Actions
{
    [Tool]
    public abstract partial class ControllerAction<R> : Resource
    {
        protected Actor Actor { get; private set; }
        
        public virtual void CustomInit(Actor actor)
        {
            Actor = actor;
        }
        
        public virtual void Reactivate(Actor actor) {}
        
        public virtual bool CanDo() => true;

        public virtual bool WantsToDo() => true;

        public bool ShouldDo() => WantsToDo() && CanDo();

        public abstract R Do(double delta);
    }
}