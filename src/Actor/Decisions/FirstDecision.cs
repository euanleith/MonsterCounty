using System.Linq;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
    public class FirstDecision<C, R> : Decision<C, R> where C : ActionController<R>
    {
        public Choice<R> Choose(C controller)
        {
            return new Choice<R>(controller.Actions.FirstOrDefault(action => action.ShouldDo()));
        }
    }
}