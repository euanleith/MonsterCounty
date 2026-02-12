using System;
using Godot;

namespace MonsterCounty.Actor.World
{
	public partial class WorldNPC : Node2D
	{
		[Export] private PathFollow2D pathFollow;
		
		public override void _PhysicsProcess(double delta)
		{
			float speed = 0.5f;
			pathFollow.ProgressRatio += speed * (float)delta;
			if (pathFollow.ProgressRatio > 1f) pathFollow.ProgressRatio = 0f;
			GlobalPosition = pathFollow.GlobalPosition;
			Rotation = pathFollow.Rotation + (float)Math.PI/2f;
		}
	}
}
