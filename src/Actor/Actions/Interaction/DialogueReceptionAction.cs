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
            // Dialogic.start("chapterA");
            Node dialogicSingleton = GetNode("/root/Dialogic");
            dialogicSingleton.Call("start", "timeline");

            // get_viewport().set_input_as_handled()
            return null; // todo a bit confusing, maybe should just return CustomVoid
        }
    }
}