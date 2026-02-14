using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Actor.World;

namespace MonsterCounty.Scene
{
    public partial class Portal : Area2D
    {
        [Export] private string _dstScene; // using PackedScene causes circular dependency - https://github.com/godotengine/godot/issues/104769
        [Export] private string _dstPortal;

        public override void _Ready()
        {
            BodyEntered += OnBodyEntered;
        }
        
        private void OnBodyEntered(Node2D body)
        {
            WorldPlayer.Instance.Get().Controllers.Get<SpawnController>().SpawnName = _dstPortal;
            SceneManager.ChangeScene(GetTree(), _dstScene);
        }
    }
}