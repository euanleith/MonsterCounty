using Godot;
using MonsterCounty.Actor.Controllers;
using static MonsterCounty.Utilities.InputManager;
using static MonsterCounty.Utilities.VectorUtilities;

namespace MonsterCounty.Actor.Actions.Movement
{
	[GlobalClass]
	public partial class PlayerMovementAction : ControllerAction<Vector2>
	{
		public override Vector2 Do(double delta)
		{
			MovementController movementController = Actor.Controllers.Get<MovementController>();
			Vector2 velocity = ClampDirection(GetMovementInput());
			if (GetRunInput())
			{
				movementController.IsRunning = true;
				velocity *= movementController.RunSpeed;
			}
			else
			{
				movementController.IsRunning = false;
				velocity *= movementController.Speed;
			}
			return Actor.Position + velocity * (float)delta;
		}
	}
}
