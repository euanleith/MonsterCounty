using Godot;
using MonsterCounty.Scene;

namespace MonsterCounty.Actor.Controllers
{
	public partial class Controller<A> : ConcreteController
		where A : Actor
	{
		public A Actor { get; private set; }
		
		public override void Load(Actor actor)
		{
			Actor = (A)actor;
			SceneManager.Instance.Get().Connect(SceneManager.SignalName.SceneChange, Callable.From(Save)); // using this rather than '+=' syntax as that still runs after the node is freed  
		}

		protected virtual void Save()
		{
			// do nothing
		}

		public override void _Process(double delta)
		{
			// do nothing
		}
	}
}
