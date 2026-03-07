using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	public abstract partial class ActionController<R> : Controller
	{
		[Export] public Array<ControllerAction<R>> Actions = []; // todo enforce that actions are of same type as controller
		[Export] protected Decision<ActionController<R>, R> Decision = new FirstDecision<ActionController<R>, R>();
		
		private ControllerAction<R> _currentAction;

		public override void Load(Actor actor)
		{
			base.Load(actor);
			LoadActions();
		}

		protected virtual void LoadActions()
		{
			if (Actions == null) return;
			foreach (ControllerAction<R> action in Actions)
			{
				action.CustomInit(Actor);
			}
		}
		
		protected virtual ControllerAction<R> NextAction()
		{
			ControllerAction<R> newAction = Decision.Choose(this).Action;
			if (newAction != null && newAction != _currentAction) newAction.Reactivate(Actor);
			_currentAction = newAction;
			return newAction;
		}
	}
}
