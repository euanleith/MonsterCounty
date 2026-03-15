using MonsterCounty.Actor.World;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Controllers
{
    public partial class ReceptionController : ActionController<CustomVoid, WorldActor>
    {
        public void Respond(double delta)
        {
            NextAction()?.Do(delta);
        }
    }
}