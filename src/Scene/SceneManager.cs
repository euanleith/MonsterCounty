using System;
using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.Scene
{
    public partial class SceneManager : Node
    {
        public static readonly Singleton<SceneManager> Instance = new();

        [Signal] public delegate void SceneChangeEventHandler();
        [Signal] public delegate void SceneCleanupEventHandler();
        [Signal] public delegate void SceneChangedEventHandler();
        
        public static string CurrentWorldScene;

        public override void _Ready()
        {
            if (!Instance.Create(this, true)) return;
            CurrentWorldScene = GetTree().CurrentScene.SceneFilePath;
        }

        public void ChangeScene(SceneTree sceneTree, PackedScene newScene)
        {
            ChangeScene(sceneTree.ChangeSceneToPacked, sceneTree, newScene);
        }

        public void ChangeScene(SceneTree sceneTree, string newScene)
        {
            ChangeScene(sceneTree.ChangeSceneToFile, sceneTree, newScene);
        }

        private async void ChangeScene<T>(Func<T, Error> changeScene, SceneTree sceneTree, T newScene)
        {
            if (newScene == null)
            {
                GD.PushError("Failed to change scene - scene reference is missing.");
                return;
            }
            CurrentWorldScene = sceneTree.CurrentScene.SceneFilePath;
            EmitSignalSceneChange();
            EmitSignalSceneCleanup();
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            changeScene.Invoke(newScene);
            EmitSignalSceneChanged();
        }
    }
}