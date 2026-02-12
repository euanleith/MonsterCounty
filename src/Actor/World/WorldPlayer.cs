using Godot;

namespace MonsterCounty.Actor.World
{
	public partial class WorldPlayer : WorldActor
	{
		public static WorldPlayer Instance { get; private set; } // todo move to Singleton class?
		
		public override void CustomInit(World world)
		{
			base.CustomInit(world);
			if (Instance == null) Instance = this;
			else
			{
				GD.PushWarning("Attempted to start more than one player");
				QueueFree();
				return;
			}
			base.CustomInit(world);
		}
	
		public void Start()
		{
			Position = World.StartPosition.Position; // todo move to movementcontroller
			Show(); // todo move to visualcontroller
			GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false; // todo move
		}
	
		public override void _Process(double delta)
		{
			base._Process(delta);
			// // todo move to visual controller
			// var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			// if (Velocity.Length() > 0)
			// {
			// 	animatedSprite2D.Play();
			// }
			// else
			// {
			// 	animatedSprite2D.Stop();
			// }
		}
	}
}
