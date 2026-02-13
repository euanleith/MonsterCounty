namespace MonsterCounty.Actor.Controllers
{
    public partial class ReceptionController : InteractionController<ReceptionController, TransmissionController>
    {
        public void Respond(double delta)
        {
            CurrentAction?.Do(delta);
        }
    }
}