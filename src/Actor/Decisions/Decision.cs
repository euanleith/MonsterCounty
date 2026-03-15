using Godot;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
    public abstract partial class Decision<C, R, A> : Resource 
        where A : Actor
        where C : ActionController<R, A>
    {
        public abstract Choice<R, A> Choose(C controller);
    }
}