using Godot;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
    public abstract partial class Decision<C, R> : Resource where C : ActionController<R>
    {
        public abstract Choice<R> Choose(C controller);
    }
}