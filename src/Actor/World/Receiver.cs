using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
	public partial class Receiver : Actor
	{
		protected override TypeMap<Controller> LoadControllers()
		{
			TypeMap<Controller> controllers = new TypeMap<Controller>();
			controllers.Add(GetNode<ReceptionController>("ReceptionController"));
			return controllers;
		}
	}
}
