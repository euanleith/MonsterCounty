using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions.Interaction;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Controllers
{
    public partial class ReceptionController : ActionController<CustomVoid, WorldActor, ReceptionAction>
    {
        [Export] public override Array<ReceptionAction> Actions { get; protected set; } = [];
        
        public void Respond(double delta)
        {
            NextAction()?.Do(delta);
        }
    }
}