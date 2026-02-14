using System;
using Godot;

namespace MonsterCounty.Scene
{
    public static class SceneManager
    {
        public static event Action SceneChanging;
        public static event Action SceneCleanup;
        public static event Action SceneChanged;

        public static void ChangeScene(SceneTree sceneTree, PackedScene newScene)
        {
            ChangeScene(sceneTree.ChangeSceneToPacked, sceneTree, newScene);
        }

        public static void ChangeScene(SceneTree sceneTree, string newScene)
        {
            ChangeScene(sceneTree.ChangeSceneToFile, sceneTree, newScene);
        }

        private static async void ChangeScene<T>(Func<T, Error> changeScene, SceneTree sceneTree, T newScene)
        {
            if (newScene == null)
            {
                GD.PushError("Failed to change scene - scene reference is missing.");
                return;
            }
            SceneChanging?.Invoke();
            SceneCleanup?.Invoke();
            await sceneTree.ToSignal(sceneTree, SceneTree.SignalName.ProcessFrame);
            changeScene.Invoke(newScene);
            await sceneTree.ToSignal(sceneTree, SceneTree.SignalName.ProcessFrame);
            SceneChanged?.Invoke();
        }
    }
}