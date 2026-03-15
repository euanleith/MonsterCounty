using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	public abstract partial class ActionController<R, A> : Controller<A>
		where A : Actor
	{
		[Export] public Array<ControllerAction<R, A>> Actions = []; // todo enforce that actions are of same type as controller
		[Export] protected Decision<ActionController<R, A>, R, A> Decision = new FirstDecision<ActionController<R, A>, R, A>();
		
		private ControllerAction<R, A> _currentAction;

		public override void Load(Actor actor)
		{
			base.Load(actor);
			LoadActions();
		}

		protected virtual void LoadActions()
		{
			if (Actions == null) return;
			foreach (ControllerAction<R, A> action in Actions)
			{
				action.CustomInit(Actor);
			}
		}
		
		protected virtual ControllerAction<R, A> NextAction()
		{
			ControllerAction<R, A> newAction = Decision.Choose(this).Action;
			if (newAction != null && newAction != _currentAction) newAction.Reactivate(Actor);
			_currentAction = newAction;
			return newAction;
		}
	}
}
