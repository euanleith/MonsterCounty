using MonsterCounty.Model;

namespace MonsterCounty.Actor.Combat
{
	public partial class CombatPlayer : CombatActor
	{
		public static readonly Singleton<CombatPlayer> Instance = new();

		public override void Load()
		{
			if (!Instance.Create(this, false)) return;
			base.Load();
		}
	}
}
