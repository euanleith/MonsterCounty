using MonsterCounty.Actor.Combat;

namespace MonsterCounty.Combat.UI
{
    public class CombatUiContext
    {
        public CombatUi Ui;
        public Party PlayerParty;
        public Party EnemyParty;
        public CombatPlayer CurrentPlayer;
        public int SelectedAction;
        public int SelectedTarget;
    }
}