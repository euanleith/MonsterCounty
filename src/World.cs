using Godot;

public partial class World : Node
{
	[Export] public PackedScene MobScene { get; set; }
	[Export] public PackedScene PlayerScene { get; set; }
	private Player _player;
	[Export] public PackedScene HUDScene { get; set; }
	private HUD _hud;

	public int Score { get; private set; }
	public Marker2D StartPosition { get; private set; }

	public override void _Ready()
	{
		Score = 0;
		StartPosition = GetNode<Marker2D>("StartPosition");
		
		_player = PlayerScene.Instantiate<Player>(); 
		_player.Init(this);
		AddChild(_player);
		
		_hud = HUDScene.Instantiate<HUD>();
		_hud.Init(this);
		AddChild(_hud);
	}

	public void NewGame()
	{
		_hud.Start();
		_player.Start();

		// todo
		GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
		
		GetNode<Timer>("StartTimer").Start();
	}
	
	public void OnMobTimerTimeout()
	{
		// Create a new instance of the Mob scene.
		Enemy enemy = MobScene.Instantiate<Enemy>();

		// Choose a random location on Path2D.
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		enemy.Position = mobSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		enemy.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		enemy.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(enemy);	}
	
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
