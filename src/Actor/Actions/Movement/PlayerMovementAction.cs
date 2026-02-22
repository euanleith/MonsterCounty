using Godot;
using MonsterCounty.Actor.Controllers;
using static MonsterCounty.Utilities.InputManager;
using static MonsterCounty.Utilities.VectorUtilities;

namespace MonsterCounty.Actor.Actions.Movement
{
	public partial class PlayerMovementAction : ControllerAction<Vector2>
	{
		public override Vector2 Do(double delta)
		{
			Vector2 velocity = ClampDirection(GetMovementInput()) * Actor.Controllers.Get<MovementController>().Speed;
			return Actor.Position + velocity * (float)delta;
		}
	}
}
