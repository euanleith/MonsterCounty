using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using static MonsterCounty.Utilities.SceneUtilities;

namespace MonsterCounty.State
{
	public struct CombatActorState
	{
		public readonly int MaxHealth;
		public readonly int Health;
		public readonly string[] ActionScenePaths;
		public readonly string WorldPath;

		public CombatActorState(int maxHealth, int health, string[] actionScenePaths, string worldPath)
		{
			MaxHealth = maxHealth;
			Health = health;
			ActionScenePaths = actionScenePaths;
			WorldPath = worldPath;
		}
		
		public CombatActorState(string scenePath) : this()
		{
			var actor = InstantiateFromScenePath<CombatActor>(scenePath);
			actor.Load();
			var combatController = actor.Controllers.Get<CombatController>();
			MaxHealth = combatController.MaxHealth;
			Health = combatController.MaxHealth;
			ActionScenePaths = new string[combatController.Actions.Count];
			for (var i = 0; i < combatController.Actions.Count; i++)
			{
				ActionScenePaths[i] = combatController.Actions[i].SceneFilePath;
			}
			// WorldScenePath = GetNodeId(actor); // todo idk what to do here, but this function is only temp anyway
		}

		public CombatActorState(Actor.Actor actor) : this()
		{
			var combatController = actor.Controllers.Get<CombatController>();
			MaxHealth = combatController.MaxHealth;
			Health = combatController.MaxHealth;
			ActionScenePaths = new string[combatController.Actions.Count];
			for (var i = 0; i < combatController.Actions.Count; i++)
			{
				ActionScenePaths[i] = combatController.Actions[i].SceneFilePath;
			}
			WorldPath = GetNodeId(actor);
		}
	}
}
