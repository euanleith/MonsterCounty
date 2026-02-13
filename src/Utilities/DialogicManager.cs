using Godot;
using MonsterCounty.Model;

namespace MonsterCounty.Utilities
{
    public partial class DialogicManager : Node
    {
        public static readonly Singleton<DialogicManager> Instance = new();

        private Node _dialogic;

        public override void _Ready()
        {
            if (!Instance.Create(this)) return;
            _dialogic = GetRoot();
            _dialogic.ProcessMode = ProcessModeEnum.Always;
            _dialogic.Connect("timeline_ended", Callable.From(TimelineEnded));
        }
        
        private Node GetRoot()
        {
            return GetNode<Node>("/root/Dialogic");
        }
        
        public void StartTimeline(string name)
        {
            var timeline = _dialogic.Call("start", name);
            (timeline.AsGodotObject() as Node).ProcessMode = ProcessModeEnum.Always;
        }

        public void TimelineEnded()
        {
            GetTree().Paused = false;
        }
    }
}