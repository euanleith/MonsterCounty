using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Combat
{
	public abstract partial class CombatActor : Actor
	{
		public CombatController CombatController;
		public CombatVisualController VisualController;
		
		public override void Load()
		{
			base.Load();
			VisualController.Hide();
		}

		public void LoadCombat(CombatActorState state)
		{
			CombatController.LoadGameState(state);
			VisualController.Show();
		}
		
		protected override TypeMap<ConcreteController> LoadControllers()
		{
			TypeMap<ConcreteController> controllers = new TypeMap<ConcreteController>();
			CombatController = GetNode<CombatController>("CombatController");
			VisualController = GetNode<CombatVisualController>("VisualController");
			controllers.Add(CombatController);
			controllers.Add(VisualController);
			return controllers;
		}
	}
}
