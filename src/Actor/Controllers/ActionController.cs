using System.Linq;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	public abstract partial class ActionController<R> : Controller
	{
		public Array<ControllerAction<R>> Actions; // todo enforce that actions are of same type as controller
		public ControllerAction<R> CurrentAction { get; private set; }
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
			Decision = GetDecision();
		}

		protected Decision<R> GetDecision()
		{
			return new FirstDecision<R>();
		}
		
		public override void _Process(double delta)
		{
			base._Process(delta);
			ControllerAction<R> newAction = Decision.Choose(Actions);
			if (newAction != null && newAction != CurrentAction) newAction.Reactivate(Actor);
			CurrentAction = newAction;
		}
	}
}
