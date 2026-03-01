using System.Collections.Generic;
using System.Linq;
using Godot;
using MonsterCounty.Model;
using MonsterCounty.State;
using MonsterCounty.Utilities;
using static MonsterCounty.Utilities.SceneUtilities;

namespace MonsterCounty.Scene
{
    public abstract partial class Scene : Node
    {
        private readonly List<string> _loadOrder = [Group.PLAYER, Group.NPC, Group.ENEMY, Group.INTERACTABLE, Group.UI];
        
        public override void _Ready()
        {
            if (GameState.EntitiesRemoved == null || GameState.EntitiesRemoved.Count == 0)
            {
                GameState.EntitiesRemoved = new Dictionary<string, List<string>>();
                _loadOrder.ForEach(group => GameState.EntitiesRemoved.Add(group, []));
            }
            _loadOrder.ForEach(LoadByGroup);
        }
        
        private void LoadByGroup(string group)
        {
            foreach (Node node in GetTree().GetNodesInGroup(group))
            {
                if (node is Loadable loadable)
                {
                    if (SceneManager.CurrentWorldScene == node.GetTree().CurrentScene.SceneFilePath &&
                        GameState.EntitiesRemoved[group].Contains(GetNodeId(node)))
                    {
                        node.QueueFree();
                    }
                    else
                    {
                        loadable.Load();
                    }
                }
            }
        }
    }
}