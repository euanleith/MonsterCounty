using MonsterCounty.Model;

namespace MonsterCounty.Actor.Controllers
{
    public partial class ReceptionController : ActionController<CustomVoid>
    {
        public void Respond(double delta)
        {
            NextAction()?.Do(delta);
        }
    }
}