using System.Linq;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	public abstract partial class ActionController<R> : Controller
	{
		public Array<ControllerAction<R>> Actions; // todo enforce that actions are of same type as controller
		private ControllerAction<R> _currentAction;
		protected Decision<ActionController<R>, R> Decision;

		public override void Load(Actor actor)
		{
			base.Load(actor);
			LoadActions();
			Decision = LoadDecision();
		}

		protected void LoadActions()
		{
			Actions = [];
			foreach (ControllerAction<R> action in GetChildren().OfType<ControllerAction<R>>())
			{
				Actions.Add(action);
				action.CustomInit(Actor);
			}
		}

		protected virtual Decision<ActionController<R>, R> LoadDecision() => new FirstDecision<ActionController<R>, R>();

		protected virtual ControllerAction<R> NextAction()
		{
			ControllerAction<R> newAction = Decision.Choose(this).Action;
			if (newAction != null && newAction != _currentAction) newAction.Reactivate(Actor);
			_currentAction = newAction;
			return newAction;
		}
	}
}
