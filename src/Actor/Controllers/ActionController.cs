using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	public abstract partial class ActionController<R> : Controller
	{
		public Array<ControllerAction<R>> Actions; // todo enforce that actions are of same type as controller
		private ControllerAction<R> _currentAction;
		protected Decision<R> Decision;

		public override void Load(Actor actor)
		{
			base.Load(actor);
			Actions = [];
			foreach (ControllerAction<R> action in GetChildren().OfType<ControllerAction<R>>())
			{
				Actions.Add(action);
				action.CustomInit(Actor);
			}
			Decision = LoadDecision();
		}

		protected virtual Decision<R> LoadDecision() => new FirstDecision<R>();

		protected ControllerAction<R> NextAction()
		{
			ControllerAction<R> newAction = Decision.Choose(Actions);
			if (newAction != null && newAction != _currentAction) newAction.Reactivate(Actor);
			_currentAction = newAction;
			return newAction;
		}
	}
}
