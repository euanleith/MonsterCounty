using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Actor.Actions.Interaction
{
    public abstract partial class TransmissionAction : ControllerAction<ReceptionController>
    {
        protected ReceptionController Responder;

        public override bool CanDo()
        {
            if (Actor.Controllers.Get<TransmissionController>().IsInteracting) return false;
            Actor hit = Raycast();
            Responder = hit?.Controllers.Get<ReceptionController>();
            return Responder != null;
        }

        private Actor Raycast()
        {
            Vector2 start = Actor.Position;
            Vector2 end = start + Vector2.FromAngle(Actor.Rotation).Normalized() * Actor.Controllers.Get<TransmissionController>().Range;
            PhysicsRayQueryParameters2D query = PhysicsRayQueryParameters2D.Create(start, end);
            Dictionary result = Actor.GetWorld2D().DirectSpaceState.IntersectRay(query);
            if (result.Count == 0) return null;
            return result["collider"].AsGodotObject() as Actor;
        }
        
        public override ReceptionController Do(double delta)
        {
            Responder.Respond(delta);
            return Responder;
        }
    }
}