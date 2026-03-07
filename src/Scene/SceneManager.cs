using System;
using Godot;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;
using MonsterCounty.State;
using static MonsterCounty.Utilities.SceneUtilities;

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
		}

		public async void ChangeToWorldScene(SceneTree sceneTree, string newScene)
		{
			GameState.EntitiesRemoved = null;
			ChangeScene(sceneTree, newScene);
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame); // todo ew - want to set CurrentWorldScene to newScene only after Scene._Ready() for newScene has run
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
			CurrentWorldScene = newScene;
		}

		public void ReturnToWorldScene(SceneTree sceneTree, string newScene)
		{
			ChangeScene(sceneTree, newScene);
		}

		public void ChangeToCombatScene(SceneTree sceneTree, WorldActor enemy)
		{
			// todo move to WorldActor.save
			enemy.Party.Save(enemy);
			WorldPlayer.Instance.Get().Party.Save(WorldPlayer.Instance.Get());
			CurrentWorldScene = GetTree().CurrentScene.SceneFilePath;
			ChangeScene(sceneTree, COMBAT_SCENE_PATH);
		}

		private void ChangeScene(SceneTree sceneTree, PackedScene newScene)
		{
			ChangeScene(sceneTree.ChangeSceneToPacked, newScene);
		}

		private void ChangeScene(SceneTree sceneTree, string newScene)
		{
			ChangeScene(sceneTree.ChangeSceneToFile, newScene);
		}

		private async void ChangeScene<T>(Func<T, Error> changeScene, T newScene)
		{
			if (newScene == null)
			{
				GD.PushError("Failed to change scene - scene reference is missing.");
				return;
			}
			EmitSignalSceneChange();
			EmitSignalSceneCleanup();
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
			changeScene.Invoke(newScene);
			EmitSignalSceneChanged();
		}
	}
}
