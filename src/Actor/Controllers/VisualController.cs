using Godot;

namespace MonsterCounty.Actor.Controllers
{
	public partial class VisualController : Controller
	{
		public override void _Process(double delta)
		{
			base._Process(delta);
			AnimatedSprite2D animation = Actor.GetNode<AnimatedSprite2D>("AnimatedSprite2D"); // todo shouldn't be dependent on specific names
			if (animation != null)
			{
				if (Actor.Velocity.Length() > 0)
					animation.Play();
				else
					animation.Stop();
			}
		}
	}
}
