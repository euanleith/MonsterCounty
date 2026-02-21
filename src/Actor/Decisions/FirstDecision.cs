using System.Linq;
using Godot.Collections;
using MonsterCounty.Actor.Actions;

namespace MonsterCounty.Actor.Decisions
{
    public class FirstDecision<R> : Decision<R>
    {
        public ControllerAction<R> Choose(Array<ControllerAction<R>> actions)
        {
            return actions.FirstOrDefault(action => action.ShouldDo());
        }
    }
}