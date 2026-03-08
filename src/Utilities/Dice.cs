using Godot;

namespace MonsterCounty.Utilities
{
    [GlobalClass]
    public partial class Dice : Resource
    {
        [Export] public int NDice { get; set; }
        [Export] public int NSides { get; set; }

        public int Roll()
        {
            return Roll(NDice, NSides);
        }

        public static int Roll(int nDice, int nSides)
        {
            int res = 0;
            for (int i = 0; i < nDice; i++)
            {
                res += GD.RandRange(1, nSides);
            }
            return res;
        }
    }
}