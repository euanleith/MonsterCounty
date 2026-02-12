using Godot;
using MonsterCounty.UI;

namespace MonsterCounty.Actor.World
{
	public partial class World : Node
	{
		[Export] public PackedScene MobScene { get; set; }
		[Export] public PackedScene PlayerScene { get; set; }
		private WorldPlayer _worldPlayer;
		[Export] public PackedScene HUDScene { get; set; }
		private HUD _hud;

		public int Score { get; private set; }
		public Marker2D StartPosition { get; private set; }

		public override void _Ready()
		{
			Score = 0;
			StartPosition = GetNode<Marker2D>("StartPosition");
		
			_worldPlayer = PlayerScene.Instantiate<WorldPlayer>(); 
			_worldPlayer.CustomInit(this);
			AddChild(_worldPlayer);
		
			_hud = HUDScene.Instantiate<HUD>();
			_hud.CustomInit(this);
			AddChild(_hud);
		}

		public void NewGame()
		{
			_hud.Start();
			_worldPlayer.Start();

			// todo
			GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
		
			GetNode<Timer>("StartTimer").Start();
		}
	
		public void OnMobTimerTimeout()
		{
			WorldEnemy worldEnemy = MobScene.Instantiate<WorldEnemy>();
			AddChild(worldEnemy);
			PathFollow2D spawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
			worldEnemy.CustomInit(this, spawnLocation);
		}
	
		public void OnScoreTimerTimeout()
		{
			Score++;
			_hud.UpdateScore(Score);
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
