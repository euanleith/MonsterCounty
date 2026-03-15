using Godot;
using MonsterCounty.Actor.World;
using MonsterCounty.Model;
using MonsterCounty.Utilities;
using MonsterCounty.Scene;

namespace MonsterCounty.Actor.Actions.Interaction
{
    [GlobalClass]
    public partial class EnemyTransmissionAction : TransmissionAction
    {
        public override CustomVoid Do(double delta)
        {
            SceneManager.Instance.Get().ChangeToCombatScene(Actor.GetTree(), Actor);
            return null;
        }

        protected override uint GetCollisionMask() => Layers.ToLayerMask(Layers.PLAYER);
    }
}