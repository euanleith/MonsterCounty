using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
    public interface Decision<C, R> where C : ActionController<R>
    {
        public Choice<R> Choose(C controller);
    }
}