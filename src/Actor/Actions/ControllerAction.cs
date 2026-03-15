using Godot;

namespace MonsterCounty.Actor.Actions
{
    public abstract partial class ControllerAction<R, A> : Resource
    {
        protected A Actor { get; private set; }
        
        public virtual void CustomInit(A actor)
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