using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
    public partial class WorldEnemy : WorldActor
    {
        protected override TypeMap<Controller> LoadControllers()
        {
            TypeMap<Controller> controllers = base.LoadControllers();
            controllers.Add(GetNode<TransmissionController>("TransmissionController"));
            return controllers;
        }
    }
}