using System;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
	public abstract partial class RandomDecision<C, R, A> : Decision<C, R, A> 
		where A : Actor
		where C : ActionController<R, A>
	{
		protected readonly Random Rand = new();
		
		public override Choice<R, A> Choose(C controller)
		{
			int index = Rand.Next(0, controller.Actions.Count);
			return new Choice<R, A>(controller.Actions[index]);
		}
	}
}
