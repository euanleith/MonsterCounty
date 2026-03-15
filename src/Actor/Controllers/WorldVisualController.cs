using Godot;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;
using static MonsterCounty.Utilities.VectorUtilities;

namespace MonsterCounty.Actor.Controllers
{
    public partial class WorldVisualController : VisualController<WorldActor>
    {
        private const string WALK = "walk";
        private const string RUN = "run";
        
        public override void _Process(double delta)
        {
            base._Process(delta);
            if (Actor.MovementController is { IsMoving: true })
                Sprite.Play();
            else
                Sprite.Stop();
            Sprite.Rotation = -Actor.Rotation;
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
                _ => Sprite.Animation
            };
            string stateName = isRunning ? RUN : WALK;
            string anim = stateName + "_" + directionName;
            Sprite.Play(anim);
        }
    }
}