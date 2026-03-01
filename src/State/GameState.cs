using System.Collections.Generic;
using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.State
{
	public partial class GameState : Node
	{
		private static readonly Singleton<GameState> Instance = new();
		
		// World
		public static string PlayerSpawnName;
		public static Vector2 PlayerPosition;
		public static Dictionary<string, List<string>> EntitiesRemoved;
		
		// Combat
		public static readonly List<CombatActorState> PlayerParty = [];
		public static readonly List<CombatActorState> EnemyParty = [];
		
		public override void _Ready()
		{
			if (!Instance.Create(this, true)) return;
		}
	}
}
