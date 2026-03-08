using Godot;
using MonsterCounty.Utilities;

namespace MonsterCounty.Combat
{
    [GlobalClass]
    public partial class Weapon : Resource
    {
        [Export] public string Name;
        [Export] public Dice Dice = new();
    }
}