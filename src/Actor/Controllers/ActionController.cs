using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	// todo reorder like TActor, TAction, TResult
	// todo should just need TActor and TResult?
	public abstract partial class ActionController<TResult, TActor, [MustBeVariant] TAction> : Controller<TActor>
		where TActor : Actor
		where TAction : ControllerAction<TResult, TActor>
	{
		[Export] protected Decision<ActionController<TResult, TActor, TAction>, TResult, TActor, TAction> Decision = 
			new FirstDecision<ActionController<TResult, TActor, TAction>, TResult, TActor, TAction>();
		
		public abstract Array<TAction> Actions { get; protected set; }
		private ControllerAction<TResult, TActor> _currentAction;

		
		public override void Load(Actor actor)
		{
			base.Load(actor);
			LoadActions();
		}

		protected virtual void LoadActions()
		{
			if (Actions == null) return;
			foreach (var action in Actions)
			{
				action.CustomInit(Actor);
			}
		}
		
		protected virtual ControllerAction<TResult, TActor> NextAction()
		{
			var newAction = Decision.Choose(this).Action;
			if (newAction != null && newAction != _currentAction) newAction.Reactivate(Actor);
			_currentAction = newAction;
			return newAction;
		}
	}
}
