using Godot;
using MonsterCounty.Model;
using MonsterCounty.Utilities;

namespace MonsterCounty.Actor.Actions.Interaction
{
	[GlobalClass]
	public partial class DialogueReceptionAction : ReceptionAction
	{
		public override CustomVoid Do(CustomVoid context)
		{
			DialogicManager.Instance.Get().StartTimeline("timeline");
			return null;
		}
	}
}
