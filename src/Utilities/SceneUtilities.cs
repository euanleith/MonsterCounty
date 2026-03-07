using Godot;

namespace MonsterCounty.Utilities
{
	public static class SceneUtilities
	{
		public const string COMBAT_SCENE_PATH = "res://scenes/combat/combat.tscn";
		public const string META_INSTANCE_RESOURCE_PATH = "InstanceResourcePath"; // resource path for resource instances. it is set to the same path as the resource itself
		
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
