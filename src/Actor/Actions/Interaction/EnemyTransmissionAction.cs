using Godot;
using MonsterCounty.Actor.Controllers;
using MonsterCounty.Utilities;
using static MonsterCounty.Utilities.SceneManager;

namespace MonsterCounty.Actor.Actions.Interaction
{
    public partial class EnemyTransmissionAction : TransmissionAction
    {
        [Export] public PackedScene CombatScene { get; set; } // todo move this somewhere
        
        public override ReceptionController Do(double delta)
        {
            ChangeScene(GetTree(), CombatScene);
            return Responder;
        }

        protected override uint GetCollisionMask() => Layers.ToLayerMask(Layers.PLAYER); // todo this isn't working?
    }
}