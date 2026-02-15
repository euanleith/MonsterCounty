using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Model;
using MonsterCounty.Scene;
using MonsterCounty.State;

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
            GameState.PlayerSpawnName = SpawnController.SpawnType.CurrentPosition.GetStringValue();
            SceneManager.Instance.Get().ChangeScene(GetTree(), SceneManager.CurrentWorldScene);
        }
    }
}