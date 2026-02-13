using Godot;

namespace MonsterCounty.Utilities
{
    public static class SceneManager
    {
        public static void ChangeScene(SceneTree sceneTree, PackedScene newScene)
        {
            sceneTree.ChangeSceneToPacked(newScene);
        }
    }
}