using Godot;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
    // todo reorder like TController, TActor, TAction, TResult
    // todo then can't this all just be under TActor or TController?
    public abstract partial class Decision<TController, TResult, TActor, TAction, TContext> : Resource 
        where TActor : Actor
        where TController : ActionController<TResult, TActor, TAction, TContext>
        where TAction : ControllerAction<TResult, TActor, TContext>
    {
        public abstract Choice<TAction> Choose(TController controller);
    }
}