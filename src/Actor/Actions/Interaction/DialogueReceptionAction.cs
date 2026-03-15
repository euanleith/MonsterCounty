using Godot;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;
using MonsterCounty.Utilities;

namespace MonsterCounty.Actor.Actions.Interaction
{
	[GlobalClass]
	public partial class DialogueReceptionAction : ControllerAction<CustomVoid, WorldActor>
	{
		public override CustomVoid Do(double delta)
		{
			DialogicManager.Instance.Get().StartTimeline("timeline");
			return null;
		}
	}
}
