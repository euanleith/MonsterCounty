using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.Actor.World
{
	public partial class WorldPlayer : WorldActor
	{
		public static Singleton<WorldPlayer> Instance = new();
		
		public override void CustomInit(World world)
		{
			if (!Instance.Create(this)) return;
			base.CustomInit(world);
		}
	
		public void Reset()
		{
			Position = World.StartPosition.Position;
		}
	}
}
