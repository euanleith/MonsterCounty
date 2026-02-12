using Godot;

namespace MonsterCounty.Actor.World
{
	public partial class WorldEnemy : WorldActor
	{
		public override void _Ready()
		{
			base._Ready();
			var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
			animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
		}
	
		private void OnVisibleOnScreenNotifier2DScreenExited()
		{
			QueueFree();
		}

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			MoveAndSlide();
		}
	}
}
