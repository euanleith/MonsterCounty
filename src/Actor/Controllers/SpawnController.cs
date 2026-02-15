using System.Collections.Generic;
using System.ComponentModel;
using Godot;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Controllers
{
	public partial class SpawnController : Controller
	{
		[Export] public string SpawnName;

		public enum SpawnType
		{
			[Description(null)] Testing,
			[Description("CurrentPosition")] CurrentPosition
		}

		public Dictionary<SpawnType, string> SpawnTypes;
		
		public static readonly string DefaultSpawnName = null;

		public override void Load(Actor actor)
		{
			base.Load(actor);
			MoveToSpawn(GameState.PlayerSpawnName);
		}

		protected override void Save()
		{
			base.Save();
			GameState.PlayerSpawnName = SpawnName;
		}

		public void MoveToSpawn(string spawnName)
		{
			if (spawnName == SpawnType.Testing.GetStringValue()) return;
			if (spawnName == SpawnType.CurrentPosition.GetStringValue())
			{
				Actor.GlobalPosition = GameState.PlayerPosition;
				return;
			}
			foreach (Node node in GetTree().GetNodesInGroup("spawn"))
			{
				if (node is not Node2D node2D || node2D.Name != spawnName) continue;
				Actor.GlobalPosition = node2D.GlobalPosition;
				SpawnName = spawnName;
				return;
			}
			GD.PushError($"{Actor.Name}: No spawn with name '{spawnName}' in current scene, keeping current spawn name '{SpawnName}'.");
		}
	}
}
