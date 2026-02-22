using System;
using Godot;

namespace MonsterCounty.Utilities
{
    public static class VectorUtilities
    {
        public static float GetRotation(Vector2 v1, Vector2 v2)
        {
            return (v1 - v2).Angle();
        }

        public static Vector2 GetDirection(Vector2 v1, Vector2 v2)
        {
            return (v1 - v2).Normalized();
        }
        
        public static Vector2 ClampDirection(Vector2 vector, int dimensions=8) {
            if (vector == Vector2.Zero) return vector;
            double angle = vector.Angle();
            double normalised = Mathf.PosMod(angle / (Math.PI * 2.0), 1.0);
            angle = Mathf.Round(normalised * dimensions)*Math.PI*2f / dimensions;
            vector.X = (float)Math.Round(Mathf.Cos(angle),6);
            vector.Y = (float)Math.Round(Mathf.Sin(angle),6);
            return vector;
        }
    }
}