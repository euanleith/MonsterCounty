using Godot;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;

namespace MonsterCounty.UI
{
	public partial class HUD : CanvasLayer
	{
		public static readonly Singleton<HUD> Instance = new();
		
		[Signal] public delegate void StartGameEventHandler();

		private World _world;

		public void CustomInit(World world)
		{
			if (!Instance.Create(this)) return;
			_world = world;
		}

		public void Reset()
		{
			UpdateScore(_world.Score);
			ShowMessage("Get Ready!");
		}
	
		public void ShowMessage(string text)
		{
			var message = GetNode<Label>("Message");
			message.Text = text;
			message.Show();

			GetNode<Timer>("MessageTimer").Start();
		}

		async public void ShowGameOver()
		{
			ShowMessage("Game Over");

			var messageTimer = GetNode<Timer>("MessageTimer");
			await ToSignal(messageTimer, Timer.SignalName.Timeout);

			var message = GetNode<Label>("Message");
			message.Text = "AAAAHHHH!!!!";
			message.Show();

			await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
			GetNode<Button>("StartButton").Show();
		}
	
		public void UpdateScore(int score)
		{
			GetNode<Label>("ScoreLabel").Text = score.ToString();
		}
	
		private void OnStartButtonPressed()
		{
			GetNode<Button>("StartButton").Hide();
			_world.NewGame();
		}

		private void OnMessageTimerTimeout()
		{
			GetNode<Label>("Message").Hide();
		}
	}
}
