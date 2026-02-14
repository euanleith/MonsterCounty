using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.Scene;

namespace MonsterCounty.Actor.World
{
    public abstract partial class WorldActor : Actor
    {
        protected WorldScene WorldScene;
        
        protected override TypeMap<Controller> LoadControllers()
        {
            TypeMap<Controller> controllers = new TypeMap<Controller>();
            controllers.Add(GetNode<MovementController>("MovementController"));
            controllers.Add(GetNode<VisualController>("VisualController"));
            return controllers;
        }
    }
}