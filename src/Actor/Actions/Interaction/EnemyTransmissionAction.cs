using Godot;
using MonsterCounty.Model;
using MonsterCounty.Utilities;
using MonsterCounty.Scene;

namespace MonsterCounty.Actor.Actions.Interaction
{
    public partial class EnemyTransmissionAction : TransmissionAction
    {
        [Export] public PackedScene CombatScene { get; set; } // todo move this somewhere
        
        public override CustomVoid Do(double delta)
        {
            SceneManager.ChangeScene(GetTree(), CombatScene);
            return null;
        }

        protected override uint GetCollisionMask() => Layers.ToLayerMask(Layers.PLAYER);
    }
}