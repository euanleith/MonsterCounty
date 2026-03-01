using Godot;

namespace MonsterCounty.Utilities
{
	public static class SceneUtilities
	{
		public static T InstantiateFromScenePath<T>(string path) where T : Node
		{
			var scene = GD.Load<PackedScene>(path);
			return scene.Instantiate<T>();
		}
		
		public static void AddChildFromScenePath<T>(Node self, string path) where T : Node
		{
			self.AddChild(InstantiateFromScenePath<T>(path));
		}
		
		public static string GetNodeId(Node node) => node.GetPath(); // todo not sure if i want to do it this way
	}
}
