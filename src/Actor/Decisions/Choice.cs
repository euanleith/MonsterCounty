using MonsterCounty.Actor.Actions;

namespace MonsterCounty.Actor.Decisions
{
    public class Choice<R>(ControllerAction<R> action)
    {
        public readonly ControllerAction<R> Action = action;
    }
}