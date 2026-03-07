using System.Linq;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
    public partial class FirstDecision<C, R> : Decision<C, R> where C : ActionController<R>
    {
        public override Choice<R> Choose(C controller)
        {
            return new Choice<R>(controller.Actions.FirstOrDefault(action => action.ShouldDo()));
        }
    }
}