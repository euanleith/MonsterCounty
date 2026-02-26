using System;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;

namespace MonsterCounty.Actor.Decisions
{
	public class RandomDecision<R> : Decision<R>
	{
		public ControllerAction<R> Choose(Array<ControllerAction<R>> actions)
		{
			Random rand = new Random();
			int index = rand.Next(0, actions.Count);
			return actions[index];
		}
	}
}
