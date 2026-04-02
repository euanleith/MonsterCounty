using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions.Interaction;
using MonsterCounty.Actor.Decisions;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Controllers
{
	public partial class TransmissionController : InstancedActionController<CustomVoid, WorldActor, TransmissionAction, CustomVoid>
	{
		[Export] public override Array<TransmissionAction> Actions { get; protected set; } = [];
		[Export] protected override Decision<
			ActionController<CustomVoid, WorldActor, TransmissionAction, CustomVoid>, 
			CustomVoid, 
			WorldActor, 
			TransmissionAction, 
			CustomVoid
		> Decision { get; set; } = new FirstDecision<
			ActionController<CustomVoid, WorldActor, TransmissionAction, CustomVoid>, 
			CustomVoid, 
			WorldActor, 
			TransmissionAction, 
			CustomVoid>();
		[Export] public float Range { get; private set; }
		
		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			NextAction()?.Do(null);
		}
	}
}
