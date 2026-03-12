using Godot;
using MonsterCounty.Model;
using static MonsterCounty.Utilities.VectorUtilities;

namespace MonsterCounty.Actor.Controllers
{
	public partial class VisualController : Controller
	{
		private AnimatedSprite2D _sprite;
		
		private const string WALK = "walk";
		private const string RUN = "run";
		
		public override void Load(Actor actor)
		{
			base.Load(actor);
			_sprite = Actor.GetNode<AnimatedSprite2D>("AnimatedSprite2D"); // todo shouldn't be dependent on specific names. i think $mynode is better though?
		}
		
		public override void _Process(double delta)
		{
			base._Process(delta);
			if (_sprite != null)
			{
				if (Actor.Controllers.Get<MovementController>() is { IsMoving: true })
					_sprite.Play();
				else
					_sprite.Stop();
				_sprite.Rotation = -Actor.Rotation;
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

		public void UpdateAnimation(Vector2 direction, bool isRunning)
		{
			if (direction == Vector2.Zero) return;
			direction = ClampDirection(direction).Normalized();
			string directionName = direction switch
			{
				{ X: > 0.5f } => Direction.RIGHT,
				{ X: < -0.5f } => Direction.LEFT,
				{ Y: > 0.5f } => Direction.DOWN,
				{ Y: < -0.5f } => Direction.UP,
				_ => _sprite.Animation
			};
			string stateName = isRunning ? RUN : WALK;
			string anim = stateName + "_" + directionName;
			_sprite.Play(anim);
		}

		public void Hide()
		{
			_sprite.Visible = false;
		}

		public void Show()
		{
			_sprite.Visible = true;
		}
	}
}
