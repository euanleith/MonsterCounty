using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
	public partial class WorldPlayer : WorldActor
	{
		public static readonly Singleton<WorldPlayer> Instance = new();
		
		public override void CustomInit(World world)
		{
			if (!Instance.Create(this)) return;
			base.CustomInit(world);
		}
	
		public void Reset()
		{
			Position = World.StartPosition.Position;
		}

		protected override TypeMap<Controller> LoadControllers()
		{
			TypeMap<Controller> controllers = base.LoadControllers();
			controllers.Add(GetNode<TransmissionController>("TransmissionController"));
			return controllers;
		}
	}
}
