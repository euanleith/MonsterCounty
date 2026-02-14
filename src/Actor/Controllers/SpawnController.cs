using Godot;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Controllers
{
	public partial class SpawnController : Controller
	{
		[Export] public string SpawnName;

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
			if (spawnName == null) return;
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
