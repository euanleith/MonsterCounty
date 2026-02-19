using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Combat
{
    public partial class CombatActor : Actor
    {
        protected override TypeMap<Controller> LoadControllers()
        {
            TypeMap<Controller> controllers = new TypeMap<Controller>();
            controllers.Add(GetNode<CombatController>("CombatController"));
            return controllers;
        }
    }
}