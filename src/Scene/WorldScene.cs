using Godot;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;
using MonsterCounty.UI;

namespace MonsterCounty.Scene
{
	public partial class WorldScene : Scene
	{
		public static readonly Singleton<WorldScene> Instance = new();
		
		[Export] public PackedScene MobScene { get; set; }

		public int Score { get; private set; }

		public override void _Ready()
		{
			if (!Instance.Create(this, false)) return;
			base._Ready();
			Score = 0;
		}

		public void NewGame()
		{
			HUD.Instance.Get().Reset();
			GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
			GetNode<Timer>("StartTimer").Start();
		}
	
		public void OnMobTimerTimeout()
		{
			WorldMob mob = MobScene.Instantiate<WorldMob>();
			AddChild(mob);
			PathFollow2D spawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
			mob.Load();
			mob.Start(spawnLocation);
		}
	
		public void OnScoreTimerTimeout()
		{
			Score++;
			HUD.Instance.Get().UpdateScore(Score);
		}
	
		public void OnStartTimerTimeout()
		{
			GetNode<Timer>("MobTimer").Start();
			GetNode<Timer>("ScoreTimer").Start();
		}
	
		public void OnPlayerHit()
		{
			GetNode<Timer>("MobTimer").Stop();
			GetNode<Timer>("ScoreTimer").Stop();
			GetNode<HUD>("HUD").ShowGameOver();
		}
	}
}
