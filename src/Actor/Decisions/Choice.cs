using MonsterCounty.Actor.Actions;

namespace MonsterCounty.Actor.Decisions
{
    public class Choice<R, A>(ControllerAction<R, A> action)
    {
        public readonly ControllerAction<R, A> Action = action;
    }
}