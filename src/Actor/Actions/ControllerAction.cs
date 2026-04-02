using Godot;

namespace MonsterCounty.Actor.Actions
{
    // todo reorder like TActor, TContext, TResult
    public abstract partial class ControllerAction<TResult, TActor, TContext> : Resource
    {
        protected TActor Actor { get; private set; }
        
        public virtual void CustomInit(TActor actor)
        {
            Actor = actor;
        }
        
        public virtual void Reactivate(Actor actor) {}
        
        public virtual bool CanDo() => true;

        public virtual bool WantsToDo() => true;

        public bool ShouldDo() => WantsToDo() && CanDo();

        public abstract TResult Do(TContext context);
    }
}