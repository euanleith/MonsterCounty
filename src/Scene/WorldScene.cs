using MonsterCounty.Model;

namespace MonsterCounty.Scene
{
	public partial class WorldScene : Scene
	{
		public static readonly Singleton<WorldScene> Instance = new();
		
		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();
		}
	}
}
