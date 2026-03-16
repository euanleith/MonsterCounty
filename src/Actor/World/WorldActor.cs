using System.Collections.Generic;
using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat;
using MonsterCounty.Model;
using MonsterCounty.Scene;
using MonsterCounty.State;

namespace MonsterCounty.Actor.World
{
	[Tool]
	public abstract partial class WorldActor : Actor
	{
		public MovementController MovementController;
		public WorldVisualController VisualController;
		public ReceptionController ReceptionController;
		
		private WorldScene _worldScene;
		public Party Party;
		
		public override void _Ready()
		{
			if (Engine.IsEditorHint())
			{
				if (FindChild("Path2D", true, false) is Path2D path)
				{
					GD.Print("found child for " + Name);
					path.Owner = GetTree().EditedSceneRoot;
				}
			}
		}

		public override void Load()
		{
			base.Load();
			Party = GetNodeOrNull<Party>("Party");
			LoadParty();
		}
		
		protected override TypeMap<ConcreteController> LoadControllers()
		{
			TypeMap<ConcreteController> controllers = new TypeMap<ConcreteController>();
			MovementController = GetNode<MovementController>("MovementController");
			VisualController = GetNode<WorldVisualController>("VisualController");
			ReceptionController = GetNode<ReceptionController>("ReceptionController");
			controllers.Add(MovementController);
			controllers.Add(VisualController);
			controllers.Add(ReceptionController);
			return controllers;
		}

		protected virtual void LoadParty()
		{
			Party?.Load(GetPartyState());
		}

		public override void Save()
		{
			Party.Save(this);	
		}

		protected abstract List<CombatActorState> GetPartyState();
	}
}
