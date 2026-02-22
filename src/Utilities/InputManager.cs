using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.Utilities
{
    public static class InputManager
    {
        public static Vector2 GetMovementInput()
        {
            Vector2 movement = Vector2.Zero;
            if (Input.IsActionPressed(Direction.RIGHT))
                movement.X = 1;
            else if (Input.IsActionPressed(Direction.LEFT))
                movement.X = -1;
            if (Input.IsActionPressed(Direction.DOWN))
                movement.Y = 1;
            else if (Input.IsActionPressed(Direction.UP))
                movement.Y = -1;
            return movement;
        }

        public static bool GetInteractInput() => Input.IsActionJustPressed("interact");
    }
}