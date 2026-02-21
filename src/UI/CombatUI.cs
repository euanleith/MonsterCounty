using Godot;
using Godot.Collections;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.Scene;

namespace MonsterCounty.UI
{
	public partial class CombatUI : CanvasLayer, Loadable
	{
		private static readonly Singleton<CombatUI> Instance = new();

		[Export] private Array<Button> _buttons;

		public void Load()
		{
			if (!Instance.Create(this, false)) return;
			
			InitCombatButtons();
		}

		private void InitCombatButtons()
		{
			CombatController player = CombatPlayer.Instance.Get().Controllers.Get<CombatController>();
			for (int i = 0; i < _buttons.Count; i++)
			{
				_buttons[i].Text = player.Actions[i].Name;
				int capturedIndex = i;
				_buttons[i].Pressed += () => CombatScene.Instance.Get().ProcessTurn(player.Actions[capturedIndex]);
			}
		}
	}
}
