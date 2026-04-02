using System.Linq;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
	public partial class FirstDecision<TController, TResult, TActor, TAction> : Decision<TController, TResult, TActor, TAction> 
		where TActor : Actor
		where TController : ActionController<TResult, TActor, TAction>
		where TAction : ControllerAction<TResult, TActor>
	{
		public override Choice<TAction> Choose(TController controller)
		{
			return new Choice<TAction>(controller.Actions.FirstOrDefault(action => action.ShouldDo()));
		}
	}
}
