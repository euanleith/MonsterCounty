using Godot;

namespace MonsterCounty.Actor.Controllers
{
	public partial class VisualController : Controller
	{
		public override void _Process(double delta)
		{
			base._Process(delta);
			AnimatedSprite2D animation = Actor.GetNode<AnimatedSprite2D>("AnimatedSprite2D"); // todo shouldn't be dependent on specific names. i think $mynode is better though? 
			if (animation != null)
			{
				if (Actor.Velocity.Length() > 0)
					animation.Play();
				else
					animation.Stop();
			}
		}

		public Vector2 GetSize()
		{
			Shape2D shape = Actor.GetNode<CollisionShape2D>("CollisionShape2D").Shape;
			if (shape is CapsuleShape2D capsule)
				return new Vector2(capsule.Radius*2, capsule.Height + capsule.Radius*2);
			if (shape is CircleShape2D circle)
				return new Vector2(circle.Radius*2, circle.Radius*2);
			if (shape is RectangleShape2D rect)
				return rect.Size;
			GD.PushError("Couldn't determine size of " + Actor.Name + ", unknown CollisionShape2D");
			return Vector2.Zero;
		}
	}
}
