using Godot;
using MonsterCounty.Model;
using MonsterCounty.Utilities;

namespace MonsterCounty.Actor.Actions.Interaction
{
	[GlobalClass]
	public partial class DialogueReceptionAction : ControllerAction<CustomVoid>
	{
		public override CustomVoid Do(double delta)
		{
			DialogicManager.Instance.Get().StartTimeline("timeline");
			return null;
		}
	}
}
