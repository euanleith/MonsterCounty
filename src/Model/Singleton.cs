using Godot;
using MonsterCounty.Scene;

namespace MonsterCounty.Model
{
	public class Singleton<T> where T : Node
	{
		private T _instance;
		public T Get() => _instance;
		private bool _isAutoload;

		public bool Create(T obj, bool isAutoload)
		{
			if (_instance != null) 
			{
				GD.PushWarning($"Singleton {obj.GetType().Name} already exists. Deleting new instance.");
				obj.QueueFree();
				return false;
			}
			_instance = obj;
			_isAutoload = isAutoload;
			SceneManager.Instance.Get().SceneCleanup += Destroy;
			return true;
		}
		
		public void Destroy()
		{
			if (!_isAutoload) _instance = null;
		}
	}
}
