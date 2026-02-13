using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Actions.Interaction
{
    public abstract partial class TransmissionAction : ControllerAction<CustomVoid>
    {
        [Export] private float _range;
        
        protected ReceptionController Responder;
        private Area2D _area;
        
        protected abstract uint GetCollisionMask();

        public override void CustomInit(Actor actor)
        {
            base.CustomInit(actor);
            
            // todo move elsewhere
            _area = new Area2D();
            Actor.AddChild(_area);

            CollisionShape2D shape = new CollisionShape2D();
            RectangleShape2D rect = new RectangleShape2D();

            Vector2 size = Actor.Controllers.Get<VisualController>().GetSize();
            rect.Size = new Vector2(_range, size.X);
            shape.Shape = rect;

            _area.AddChild(shape);
            _area.CollisionMask = GetCollisionMask();
            _area.Position = new Vector2(_range/2 + size.X/2 + size.X/6, 0); // todo literally no clue why i need size.X/6 but it works so..
            _area.BodyEntered += BodyEntered;
            _area.BodyExited += BodyExited;
        }
        
        private void BodyEntered(Node2D body) => Responder = (body as Actor)?.Controllers.Get<ReceptionController>();
        private void BodyExited(Node2D body) => Responder = null;

        public override bool CanDo()
        {
            return Responder != null;
        }
    }
}