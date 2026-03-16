using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Actions.Interaction
{
	[GlobalClass]
	public abstract partial class TransmissionAction : ControllerAction<CustomVoid, WorldActor>
	{
		[Export] private float _range;
		
		protected ReceptionController Responder;
		private Area2D _hitbox;
		
		protected abstract uint GetCollisionMask();

		public override void CustomInit(WorldActor actor)
		{
			base.CustomInit(actor);
			
			_hitbox = ConstructHitbox(_range, Actor.VisualController.GetSize());
			Actor.AddChild(_hitbox);
			_hitbox.CollisionMask = GetCollisionMask();
			_hitbox.BodyEntered += BodyEntered;
			_hitbox.BodyExited += BodyExited;
		}

		private static Area2D ConstructHitbox(float range, Vector2 actorSize)
		{
			var area = new Area2D();
			var shape = new CollisionShape2D();
			var rect = new RectangleShape2D();

			rect.Size = new Vector2(range, actorSize.X);
			shape.Shape = rect;			
			
			area.AddChild(shape);			
			area.Position = new Vector2(range/2 + actorSize.X/2 + actorSize.X/6, 0); // todo literally no clue why i need size.X/6 but it works so..

			return area;
		}

		private void BodyEntered(Node2D body) => Responder = (body as WorldActor)?.ReceptionController;

		private void BodyExited(Node2D body) => Responder = null;

		public override bool CanDo()
		{
			return Responder != null;
		}
	}
}
