using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.Combat
{
    public abstract partial class CombatActor : Actor
    {
        protected override TypeMap<Controller> LoadControllers()
        {
            TypeMap<Controller> controllers = new TypeMap<Controller>();
            controllers.Add(GetNode<CombatController>("CombatController"));
            controllers.Add(GetNode<VisualController>("VisualController"));
            return controllers;
        }
    }
}