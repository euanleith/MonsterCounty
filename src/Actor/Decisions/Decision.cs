using Godot.Collections;
using MonsterCounty.Actor.Actions;

namespace MonsterCounty.Actor.Decisions
{
    public interface Decision<R>
    {
        public abstract ControllerAction<R> Choose(Array<ControllerAction<R>> actions);
    }
}