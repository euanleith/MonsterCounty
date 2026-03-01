using Godot;
using MonsterCounty.Model;
using MonsterCounty.Utilities;
using MonsterCounty.Scene;
using MonsterCounty.State;

namespace MonsterCounty.Actor.Actions.Interaction
{
    public partial class EnemyTransmissionAction : TransmissionAction
    {
        [Export] public PackedScene CombatScene { get; set; } // todo move this somewhere. maybe to SceneManager?
        
        public override CustomVoid Do(double delta)
        {
            SceneManager.Instance.Get().ChangeToCombatScene(GetTree(), CombatScene, Actor);
            return null;
        }

        protected override uint GetCollisionMask() => Layers.ToLayerMask(Layers.PLAYER);
    }
}