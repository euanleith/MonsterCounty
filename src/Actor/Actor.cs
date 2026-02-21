using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor
{
	public abstract partial class Actor : CharacterBody2D, Loadable
	{
		public TypeMap<Controller> Controllers { get; private set; }
		
		protected abstract TypeMap<Controller> LoadControllers();

		public virtual void Load()
		{
			Controllers = LoadControllers();
			Controllers.ForEach(controller => controller.Load(this));
		}
		
		public override void _Process(double delta)
		{
			Controllers.ForEach(controller => controller._Process(delta));
		}
	}
}
