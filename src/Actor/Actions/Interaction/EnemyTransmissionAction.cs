using Godot;
using MonsterCounty.Model;
using MonsterCounty.Scene;
using MonsterCounty.Utilities;

namespace MonsterCounty.Actor.Actions.Interaction
{
    [GlobalClass]
    public partial class EnemyTransmissionAction : TransmissionAction
    {
        public override CustomVoid Do(CustomVoid context)
        {
            SceneManager.Instance.Get().ChangeToCombatScene(Actor.GetTree(), Actor);
            return null;
        }

        protected override uint GetCollisionMask() => Layers.ToLayerMask(Layers.PLAYER);
    }
}