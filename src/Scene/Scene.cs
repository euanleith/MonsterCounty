using System.Collections.Generic;
using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.Scene
{
    public abstract partial class Scene : Node
    {
        private readonly List<string> _loadOrder = ["player", "npc", "enemy", "ui"];
        
        public override void _Ready()
        {
            _loadOrder.ForEach(LoadByGroup);
        }
        
        private void LoadByGroup(string group)
        {
            foreach (Node node in GetTree().GetNodesInGroup(group))
            {
                if (node is Loadable loadable)
                {
                    loadable.Load();
                }
            }
        }
    }
}