using Godot;
using MonsterCounty.Actor.Actions.Combat;
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
		public readonly string[] ActionResourcePaths;
		public readonly string WorldPath;
		public readonly CombatPosition CombatPosition;
		public readonly Weapon Weapon;

		public CombatActorState(CombatActor combatActor, WorldActor worldActor=null) : this()
		{
			var combatController = combatActor.Controllers.Get<CombatController>();
			MaxHealth = combatController.MaxHealth;
			CurrentHealth = combatController.CurrentHealth;
			ActionResourcePaths = new string[combatController.Actions.Count];
			for (var i = 0; i < combatController.Actions.Count; i++)
			{
				string resourcePath = combatController.Actions[i].ResourcePath;
				ActionResourcePaths[i] = resourcePath != "" ?
					resourcePath :
					combatController.Actions[i].GetMeta(META_INSTANCE_RESOURCE_PATH).AsString();
			}
			if (worldActor != null) WorldPath = GetNodeId(worldActor);
			CombatPosition = combatController.CombatPosition;
			Weapon = combatController.Weapon;
		}
	}
}
