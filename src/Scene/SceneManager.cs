using System;
using Godot;
using MonsterCounty.Model;
using MonsterCounty.State;

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

		public void ChangeToCombatScene(SceneTree sceneTree, PackedScene newScene, Actor.Actor enemy)
		{
			GameState.EnemyParty.Clear();
			GameState.EnemyParty.Add(new CombatActorState(enemy));
			// todo for testing, adding another enemy, and adding players statically
			GameState.EnemyParty.Add(new CombatActorState(enemy));
			// GameState.EnemyParty.Add(new CombatActorState(enemy));
			// GameState.EnemyParty.Add(new CombatActorState(enemy));
			if (GameState.PlayerParty.Count == 0)
			{
				GameState.PlayerParty.Add(new CombatActorState("res://scenes/combat/combat_player1.tscn"));
				GameState.PlayerParty.Add(new CombatActorState("res://scenes/combat/combat_player2.tscn"));
			}
			CurrentWorldScene = GetTree().CurrentScene.SceneFilePath;
			ChangeScene(sceneTree, newScene);
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
