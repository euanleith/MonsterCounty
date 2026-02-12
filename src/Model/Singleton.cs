using Godot;

namespace MonsterCounty.Model
{
    public class Singleton<T> where T : Node
    {
        private static T Instance;
        public T Get() => Instance;

        public bool Create(T obj)
        {
            if (Instance == null) Instance = obj;
            else
            {
                GD.PushWarning($"Singleton {obj.GetType().Name} already exists. Deleting new instance.");
                obj.QueueFree();
                return false;
            }
            return true;
        }
    }
}