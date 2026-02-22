using System.Linq;

namespace MonsterCounty.Utilities
{
    public static class Layers
    {
        public const uint PLAYER = 1;
        public const uint NPC = 2;
        public const uint ENEMY = 3;
        public const uint INTERACTABLE = 4;
        
        public static uint ToLayerMask(uint layer) => (uint)1 << (int)(layer-1);

        public static uint ToLayerMask(params uint[] layers) => 
            CombineLayerMasks(layers.Select(ToLayerMask).ToArray());
        
        public static uint CombineLayerMasks(params uint[] layerMasks) => 
            layerMasks.Aggregate<uint, uint>(0, (current, layerMask) => current | layerMask);
    }
}