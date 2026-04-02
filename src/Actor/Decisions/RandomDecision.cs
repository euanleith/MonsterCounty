using System;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
	public abstract partial class RandomDecision<TController, TResult, TActor, TAction> : Decision<TController, TResult, TActor, TAction> 
		where TActor : Actor
		where TController : ActionController<TResult, TActor, TAction>
		where TAction : ControllerAction<TResult, TActor>
	{
		protected readonly Random Rand = new();
		
		public override Choice<TAction> Choose(TController controller)
		{
			int index = Rand.Next(0, controller.Actions.Count);
			return new Choice<TAction>(controller.Actions[index]);
		}
	}
}
