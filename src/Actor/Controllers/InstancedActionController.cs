using Godot.Collections;
using MonsterCounty.Actor.Actions;
using static MonsterCounty.Utilities.SceneUtilities;

namespace MonsterCounty.Actor.Controllers
{
	/**
	 * Actions are resources, so are shared by default.
	 * If unique action instances are required, extend this class instead of ActionController
	 *
	 * Example use case: if you need to know which actor performed the action,
	 * e.g. TransmissionAction needs to know who is transmitting
	 */
	public abstract partial class InstancedActionController<R> : ActionController<R>
	{
		protected override void LoadActions()
		{
			if (Actions == null) return;
			var instances = new Array<ControllerAction<R>>();
			foreach (var action in Actions)
			{
				var instance = (ControllerAction<R>)action.Duplicate();
				instance.SetMeta(META_INSTANCE_RESOURCE_PATH, action.ResourcePath);
				instances.Add(instance);
			}
			Actions = instances;
			base.LoadActions();
		}
	}
}
