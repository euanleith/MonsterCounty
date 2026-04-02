using System.Linq;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
	public partial class FirstDecision<TController, TResult, TActor, TAction, TContext> : Decision<TController, TResult, TActor, TAction, TContext> 
		where TActor : Actor
		where TController : ActionController<TResult, TActor, TAction, TContext>
		where TAction : ControllerAction<TResult, TActor, TContext>
	{
		public override Choice<TAction> Choose(TController controller)
		{
			return new Choice<TAction>(controller.Actions.FirstOrDefault(action => action.ShouldDo()));
		}
	}
}
