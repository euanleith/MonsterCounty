using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
    public abstract partial class WorldActor : Actor
    {
        protected World World;

        public virtual void CustomInit(World world)
        {
            World = world;
        }

        protected override TypeMap<Controller> LoadControllers()
        {
            TypeMap<Controller> controllers = new TypeMap<Controller>();
            controllers.Add(GetNode<MovementController>("MovementController"));
            controllers.Add(GetNode<VisualController>("VisualController"));
            return controllers;
        }
    }
}