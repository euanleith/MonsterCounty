using Godot;
using MonsterCounty.Utilities;
using static MonsterCounty.Utilities.InputManager;

namespace MonsterCounty.Actor.Actions.Interaction
{
	public partial class PlayerTransmissionAction : TransmissionAction
	{
		public override bool WantsToDo() => GetInteractInput();

		protected override uint GetCollisionMask() => Layers.ToLayerMask(Layers.NPC);
	}
}
