using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
	public partial class WorldPlayer : WorldActor
	{
		public static readonly Singleton<WorldPlayer> Instance = new();

		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();
		}

		protected override TypeMap<Controller> LoadControllers()
		{
			TypeMap<Controller> controllers = base.LoadControllers();
			controllers.Add(GetNode<TransmissionController>("TransmissionController"));
			controllers.Add(GetNode<ReceptionController>("ReceptionController"));
			controllers.Add(GetNode<SpawnController>("SpawnController"));
			return controllers;
		}
	}
}
