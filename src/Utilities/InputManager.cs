using Godot;

namespace MonsterCounty.Utilities
{
    public static class InputManager
    {
        public static Vector2 GetMovementInput()
        {
            Vector2 movement = Vector2.Zero;
            if (Input.IsActionPressed("right"))
                movement.X = 1;
            else if (Input.IsActionPressed("left"))
                movement.X = -1;
            if (Input.IsActionPressed("down"))
                movement.Y = 1;
            else if (Input.IsActionPressed("up"))
                movement.Y = -1;
            return movement;
        }

        public static bool GetInteractInput() => Input.IsActionJustPressed("interact");
    }
}