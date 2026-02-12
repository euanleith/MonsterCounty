using Godot;

namespace MonsterCounty.Actor.World
{
	public partial class WorldPlayer : WorldActor
	{
		public static WorldPlayer Instance { get; private set; }
	
		[Export]
		public int Speed { get; set; } = 400;

		public Vector2 ScreenSize;

		public void Init(World world)
		{
			if (Instance == null) Instance = this;
			else
			{
				GD.PushWarning("Attempted to start more than one player");
				QueueFree();
				return;
			}

			base.Init(world);
		}
	
		public void Start()
		{
			Position = _world.StartPosition.Position;
			Show();
			GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		}
	
		public override void _Ready()
		{
			ScreenSize = GetViewportRect().Size;
		}
	
		public override void _Process(double delta)
		{
			var velocity = Vector2.Zero;

			if (Input.IsActionPressed("right"))
				velocity.X += 1;
			if (Input.IsActionPressed("left"))
				velocity.X -= 1;
			if (Input.IsActionPressed("down"))
				velocity.Y += 1;
			if (Input.IsActionPressed("up"))
				velocity.Y -= 1;

			var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

			if (velocity.Length() > 0)
			{
				velocity = velocity.Normalized() * Speed;
				animatedSprite2D.Play();
			}
			else
			{
				animatedSprite2D.Stop();
			}
		
			Position += velocity * (float)delta;
			Position = new Vector2(
				x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
				y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
			);
		}
	}
}
