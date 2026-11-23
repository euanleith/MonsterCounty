using System;
using Godot;

namespace MonsterCounty.UI
{
	[GlobalClass]
	public partial class ButtonContainer : StaticContainer<Button>
	{
		public override void Load(Action<Button, int> loadElement, Action<Button, int> rebindElement)
		{
			base.Load(loadElement, rebindElement);	
			Focus(0);
		}
		
		public void Enable()
		{
			foreach (var button in Array)
			{
				button.Disabled = false;
				button.FocusMode = FocusModeEnum.All;
			}
		}

		public void Disable()
		{
			foreach (var button in Array)
			{
				button.Disabled = true;
				button.FocusMode = FocusModeEnum.None;
			}
		}

		public bool Focus(int index)
		{
			if (index < 0 || index >= Array.Count) return false;
			Array[index].GrabFocus();
			return true;
		}
	}
}
