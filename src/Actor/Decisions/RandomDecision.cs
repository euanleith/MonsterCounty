using System;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
	public abstract partial class RandomDecision<TController, TResult, TActor, TAction, TContext> : Decision<TController, TResult, TActor, TAction, TContext> 
		where TActor : Actor
		where TController : ActionController<TResult, TActor, TAction, TContext>
		where TAction : ControllerAction<TResult, TActor, TContext>
	{
		protected readonly Random Rand = new();
		
		public override Choice<TAction> Choose(TController controller)
		{
			int index = Rand.Next(0, controller.Actions.Count);
			return new Choice<TAction>(controller.Actions[index]);
		}
	}
}
