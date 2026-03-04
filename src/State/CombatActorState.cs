using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat;
using static MonsterCounty.Utilities.SceneUtilities;

namespace MonsterCounty.State
{
	public struct CombatActorState
	{
		public readonly int MaxHealth;
		public readonly int Health;
		public readonly string[] ActionScenePaths;
		public readonly string WorldPath;
		public readonly CombatPosition CombatPosition;
		
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
			CombatPosition = combatController.CombatPosition;
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
			CombatPosition = combatController.CombatPosition;
		}
	}
}
