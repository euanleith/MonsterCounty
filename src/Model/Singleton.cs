using Godot;
using MonsterCounty.Scene;

namespace MonsterCounty.Model
{
	public class Singleton<T> where T : Node
	{
		public T Instance;
		public T Get() => Instance;
		private bool _isAutoload;

		public bool Create(T obj, bool isAutoload)
		{
			if (Instance != null) 
			{
				GD.PushWarning($"Singleton {obj.GetType().Name} already exists. Deleting new instance.");
				obj.QueueFree();
				return false;
			}
			Instance = obj;
			_isAutoload = isAutoload;
			SceneManager.SceneCleanup += Destroy;
			return true;
		}
		
		public void Destroy()
		{
			if (!_isAutoload) Instance = null;
		}
	}
}
