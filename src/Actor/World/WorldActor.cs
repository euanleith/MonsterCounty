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
		protected WorldScene WorldScene;
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
		
		protected override TypeMap<Controller> LoadControllers()
		{
			TypeMap<Controller> controllers = new TypeMap<Controller>();
			controllers.Add(GetNode<MovementController>("MovementController"));
			controllers.Add(GetNode<VisualController>("VisualController"));
			return controllers;
		}

		protected virtual void LoadParty()
		{
			Party?.Load(GetPartyState());
		}

		protected abstract List<CombatActorState> GetPartyState();
	}
}
