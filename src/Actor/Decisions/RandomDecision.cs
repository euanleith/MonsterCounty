using System;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
	public class RandomDecision<C, R> : Decision<C, R> where C : ActionController<R>
	{
		public virtual Choice<R> Choose(C controller)
		{
			Random rand = new Random();
			int index = rand.Next(0, controller.Actions.Count);
			return new Choice<R>(controller.Actions[index]);
		}
	}
}
