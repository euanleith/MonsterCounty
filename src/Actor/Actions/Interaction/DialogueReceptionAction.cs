using Godot;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Interaction
{
    public partial class DialogueReceptionAction : ControllerAction<TransmissionController>
    {
        public override TransmissionController Do(double delta)
        {
            GD.Print("talking!!!"); // todo only happening once?
            // todo start dialogue
            return null; // todo a bit confusing, maybe should just return CustomVoid
        }
    }
}