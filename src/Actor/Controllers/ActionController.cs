using System.Linq;
using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;

namespace MonsterCounty.Actor.Controllers
{
    public abstract partial class ActionController<R> : Controller
    {
        [Export] Array<ControllerAction<R>> Actions;
        
        protected ControllerAction<R> currentAction;

        public override void CustomInit(Actor actor)
        {
            base.CustomInit(actor);
            foreach (ControllerAction<R> action in Actions)
                action.CustomInit(Actor);
        }
        
        public override void _Process(double delta)
        {
            base._Process(delta);
            ControllerAction<R> newAction = Actions.FirstOrDefault(action => action.ShouldDo());
            if (newAction != null && newAction != currentAction) newAction.Reactivate(Actor);
            currentAction = newAction;
        }
    }
}