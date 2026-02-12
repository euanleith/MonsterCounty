using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor
{
    public abstract partial class Actor : CharacterBody2D
    {
        public TypeMap<Controller> Controllers { get; private set; }
        
        protected abstract TypeMap<Controller> LoadControllers();

        public override void _Ready()
        {
            CustomInit();
        }
        
        protected virtual void CustomInit()
        {
            Controllers = LoadControllers();
            Controllers.ForEach(controller => controller.CustomInit(this));
        }
        
        public override void _Process(double delta)
        {
            Controllers.ForEach(controller => controller._Process(delta));
        }
    }
}