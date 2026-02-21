using Godot;
using MonsterCounty.Model;
using MonsterCounty.Scene;

namespace MonsterCounty.UI
{
	public partial class CombatUI : CanvasLayer
	{
		private static readonly Singleton<CombatUI> Instance = new();

		[Export] private Button _toWorldButton;

		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();

			_toWorldButton.Pressed += OnToWorldButtonPressed;
		}

		private void OnToWorldButtonPressed()
		{
			CombatScene.Instance.Get().ProcessTurn();
		}
	}
}
