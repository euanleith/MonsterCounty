using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.Scene;

namespace MonsterCounty.Actor
{
	public abstract partial class Actor : CharacterBody2D, Loadable
	{
		private TypeMap<ConcreteController> _controllers;
		protected bool AutoSave = true;
		
		protected abstract TypeMap<ConcreteController> LoadControllers();

		public virtual void Load()
		{
			_controllers = LoadControllers();
			_controllers.ForEach(controller => controller.Load(this));
			SceneManager.Instance.Get().Connect(SceneManager.SignalName.SceneChange, Callable.From(InternalSave)); // using this rather than '+=' syntax as that still runs after the node is freed  
		}

		private void InternalSave()
		{
			if (AutoSave) Save();
		}

		public virtual void Save()
		{
			_controllers.ForEach(controller => controller.Save());
		}

		public override void _Process(double delta)
		{
			_controllers.ForEach(controller => controller._Process(delta));
		}
	}
}
