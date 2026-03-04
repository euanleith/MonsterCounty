using Godot;

namespace MonsterCounty.UI
{
	[GlobalClass]
	public partial class ButtonContainer : StaticContainer<Button>
	{
		public void Enable()
		{
			foreach (var button in Array)
			{
				button.Disabled = false;
			}
		}

		public void Disable()
		{
			foreach (var button in Array)
			{
				button.Disabled = true;
			}
		}
	}
}
