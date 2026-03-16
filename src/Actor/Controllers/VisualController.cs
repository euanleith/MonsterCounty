using Godot;

namespace MonsterCounty.Actor.Controllers
{
	public abstract partial class VisualController<A> : Controller<A>
		where A : Actor
	{
		protected AnimatedSprite2D Sprite;
		
		public override void Load(Actor actor)
		{
			base.Load(actor);
			Sprite = Actor.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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

		public void Hide()
		{
			Sprite.Visible = false;
		}

		public void Show()
		{
			Sprite.Visible = true;
		}
	}
}
