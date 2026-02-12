using Godot;
using MonsterCounty.Actor.Controllers;
using static MonsterCounty.Utilities.InputManager;

namespace MonsterCounty.Actor.Actions.Movement
{
	[Tool]
	public partial class PlayerMovementAction : ControllerAction<Vector2>
	{
		// public override bool CanDo() => !controllers.Get<InteractionController>().IsInteracting;
		
		public override Vector2 Do(double delta)
		{
			Vector2 velocity = GetMovementInput() * Actor.Controllers.Get<MovementController>().Speed;
			if (velocity.Length() > 0)
			{
				velocity = velocity.Normalized() * Actor.Controllers.Get<MovementController>().Speed;
			}
			return Actor.Position + velocity * (float)delta;
		}
	}
}
