using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Actions;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Controllers
{
	public partial class TransmissionController : InstancedActionController<CustomVoid, WorldActor>
	{
		[Export] public float Range { get; private set; }
		
		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			NextAction()?.Do(delta);
		}
	}
}
