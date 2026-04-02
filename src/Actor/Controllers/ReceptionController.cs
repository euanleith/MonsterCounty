using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions.Interaction;
using MonsterCounty.Actor.Decisions;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Controllers
{
    public partial class ReceptionController : ActionController<CustomVoid, WorldActor, ReceptionAction, CustomVoid>
    {
        [Export] public override Array<ReceptionAction> Actions { get; protected set; } = [];
        [Export] protected override Decision<
            ActionController<CustomVoid, WorldActor, ReceptionAction, CustomVoid>, 
            CustomVoid, 
            WorldActor, 
            ReceptionAction, 
            CustomVoid
        > Decision { get; set; } = new FirstDecision<
            ActionController<CustomVoid, WorldActor, ReceptionAction, CustomVoid>, 
            CustomVoid, 
            WorldActor, 
            ReceptionAction, 
            CustomVoid>();
        
        public void Respond()
        {
            NextAction()?.Do(null);
        }
    }
}