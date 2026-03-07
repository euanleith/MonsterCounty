using System;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Decisions
{
	public abstract partial class RandomDecision<C, R> : Decision<C, R> where C : ActionController<R>
	{
		private readonly Random _rand = new();
		
		public override Choice<R> Choose(C controller)
		{
			int index = _rand.Next(0, controller.Actions.Count);
			return new Choice<R>(controller.Actions[index]);
		}
	}
}
