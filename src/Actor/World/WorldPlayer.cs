using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
	public partial class WorldPlayer : WorldActor
	{
		public static Singleton<WorldPlayer> Instance = new();
		
		public override void CustomInit(World world)
		{
			if (!Instance.Create(this)) return;
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
