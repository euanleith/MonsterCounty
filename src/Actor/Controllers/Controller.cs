namespace MonsterCounty.Actor.Controllers
{
	public partial class Controller<TActor> : ConcreteController
		where TActor : Actor
	{
		public TActor Actor { get; private set; }
		
		public override void Load(Actor actor)
		{
			Actor = (TActor)actor;
			// SceneManager.Instance.Get().Connect(SceneManager.SignalName.SceneChange, Callable.From(Save)); // using this rather than '+=' syntax as that still runs after the node is freed  
		}

		public override void _Process(double delta)
		{
			// do nothing
		}
	}
}
