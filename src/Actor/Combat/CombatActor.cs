using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Combat
{
    public abstract partial class CombatActor : Actor
    {
        public override void Load()
        {
            base.Load();
            Controllers.Get<CombatVisualController>().Hide();
        }

        public void LoadCombat(CombatActorState state)
        {
            Controllers.Get<CombatController>().LoadGameState(state);
			Controllers.Get<CombatVisualController>().Show();
        }
        
        protected override TypeMap<Controller> LoadControllers()
        {
            TypeMap<Controller> controllers = new TypeMap<Controller>();
            controllers.Add(GetNode<CombatController>("CombatController"));
            controllers.Add(GetNode<CombatVisualController>("VisualController"));
            return controllers;
        }
    }
}