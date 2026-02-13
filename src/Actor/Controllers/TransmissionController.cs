using Godot;

namespace MonsterCounty.Actor.Controllers
{
    public partial class TransmissionController : InteractionController<TransmissionController, ReceptionController>
    {
        [Export] public float Range { get; private set; }
        
        public override void _Process(double delta)
        {
            base._Process(delta);
            if (IsInteracting) return;
            Interlocutor = CurrentAction?.Do(delta);
            if (CurrentAction != null) StartInteracting();
        }
    }
}