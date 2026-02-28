using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Combat.UI;

namespace MonsterCounty.Actor.Combat
{
	public partial class CombatPlayer : CombatActor
	{
		public override TurnResult StartTurn()
		{
			GD.Print("starting player turn");
			CombatUI.Instance.Get().NextPlayer(this);
			return new TurnResult(true);
		}
		
		public override CombatActor ResolveTurn(int index)
		{
			return Controllers.Get<CombatController>().Actions[index].Do(CombatUI.Instance.Get().SelectedEnemy);
		}
	}
}
