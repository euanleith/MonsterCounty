using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.Decisions;

namespace MonsterCounty.Actor.Controllers
{
	// todo reorder like TActor, TAction, TContext, TResult
	// todo should just need TActor and TResult?
	public abstract partial class ActionController<TResult, TActor, [MustBeVariant] TAction, TContext> : Controller<TActor>
		where TActor : Actor
		where TAction : ControllerAction<TResult, TActor, TContext>
	{
		public abstract Array<TAction> Actions { get; protected set; }
		private TAction _currentAction;
		protected abstract Decision<
			ActionController<TResult, TActor, TAction, TContext>, 
			TResult, 
			TActor, 
			TAction, 
			TContext
		> Decision { get; set; }
		
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
		
		protected TAction NextAction()
		{
			var newAction = Decision.Choose(this).Action;
			if (newAction != null && newAction != _currentAction) newAction.Reactivate(Actor);
			_currentAction = newAction;
			return newAction;
		}
	}
}
