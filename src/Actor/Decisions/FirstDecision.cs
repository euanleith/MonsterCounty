using System.Linq;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
	public partial class FirstDecision<C, R, A> : Decision<C, R, A> 
		where A : Actor
		where C : ActionController<R, A>
	{
		public override Choice<R, A> Choose(C controller)
		{
			return new Choice<R, A>(controller.Actions.FirstOrDefault(action => action.ShouldDo()));
		}
	}
}
