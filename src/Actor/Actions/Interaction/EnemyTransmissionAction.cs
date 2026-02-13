using Godot;
using MonsterCounty.Model;
using MonsterCounty.Utilities;
using static MonsterCounty.Utilities.SceneManager;

namespace MonsterCounty.Actor.Actions.Interaction
{
    public partial class EnemyTransmissionAction : TransmissionAction
    {
        [Export] public PackedScene CombatScene { get; set; } // todo move this somewhere
        
        public override CustomVoid Do(double delta)
        {
            ChangeScene(GetTree(), CombatScene);
            return null;
        }

        protected override uint GetCollisionMask() => Layers.ToLayerMask(Layers.PLAYER);
    }
}