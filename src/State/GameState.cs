using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.State
{
	public partial class GameState : Node
	{
		private static readonly Singleton<GameState> Instance = new();
		
		public static string PlayerSpawnName;
		public static Vector2 PlayerPosition;

		public override void _Ready()
		{
			if (!Instance.Create(this, true)) return;
		}
	}
}
