using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Actor.World;
using MonsterCounty.Combat;
using static MonsterCounty.Utilities.SceneUtilities;

namespace MonsterCounty.State
{
	public struct CombatActorState
	{
		public readonly int MaxHealth;
		public readonly int CurrentHealth;
		public readonly string[] ActionScenePaths;
		public readonly string WorldPath;
		public readonly CombatPosition CombatPosition;

		public CombatActorState(CombatActor combatActor, WorldActor worldActor=null) : this()
		{
			var combatController = combatActor.Controllers.Get<CombatController>();
			MaxHealth = combatController.MaxHealth;
			CurrentHealth = combatController.CurrentHealth;
			ActionScenePaths = new string[combatController.Actions.Count];
			for (var i = 0; i < combatController.Actions.Count; i++)
			{
				ActionScenePaths[i] = combatController.Actions[i].SceneFilePath;
			}
			if (worldActor != null) WorldPath = GetNodeId(worldActor);
			CombatPosition = combatController.CombatPosition;
		}
	}
}
