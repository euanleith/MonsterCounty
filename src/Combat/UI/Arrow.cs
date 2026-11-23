using Godot;
using MonsterCounty.Actor.Combat;

namespace MonsterCounty.Combat.UI
{
	public partial class Arrow : Sprite2D
	{
		[Export] private ArrowPosition _arrowPosition;
		[Export] private float _offsetY = 10;
		
		public enum ArrowPosition
		{
			Above,
			Below
		}
		
		private readonly CombatPosition[] _selectionPriority =
		[
			CombatPosition.Right,
			CombatPosition.Front, 
			CombatPosition.Back, 
			CombatPosition.Left
		];
		
		public int SelectedIndex;
		private Party _party;

		// todo how to generalise this wrt static/dynamic uielement?
		public void Load()
		{
		}

		public int Bind(Party party)
		{
			_party = party;
			return Reset();
		}

		public int LoadAndBind(Party party)
		{
			Load();
			return Bind(party);
		}

		public int Reset()
		{
			foreach (CombatPosition combatPosition in _selectionPriority)
			{
				int index = Rebind(combatPosition);
				if (index != -1) return index;
			}
			return -1;
		}
		
		public int Rebind(CombatPosition combatPosition)
		{
			int index = _party.IndexOf(combatPosition);
			if (index == -1 || !_party.Get(index).CombatController.IsAlive) return -1;
			SelectedIndex = index;
			Rebind(_party.Get(index));
			return index;
		}

		public void Rebind(CombatActor actor)
		{
			float offsetY = _offsetY + actor.VisualController.GetSize().Y/2;
			if (_arrowPosition == ArrowPosition.Above) offsetY *= -1;
			GlobalPosition = actor.GlobalPosition + new Vector2(0, offsetY);
		}
	}
}
