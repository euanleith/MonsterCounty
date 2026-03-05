using System.Collections.Generic;
using System.Linq;
using Godot;
using MonsterCounty.Actor.Combat;
using MonsterCounty.Actor.Controllers;

namespace MonsterCounty.Combat.UI
{
    public partial class Arrow : Sprite2D
    {
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
        private ArrowPosition _arrowPosition;
        private float _arrowOffsetY;

        // todo how to generalise this wrt static/dynamic uielement?
        public void Load(ArrowPosition arrowPosition, float arrowOffsetY)
        {
            _arrowPosition = arrowPosition;
        }

        public void Bind(Party party)
        {
            _party = party;
            Reset();
        }

        public void LoadAndBind(ArrowPosition arrowPosition, float arrowOffsetY, Party party)
        {
            Load(arrowPosition, arrowOffsetY);
            Bind(party);
        }

        public void Reset()
        {
            foreach (CombatPosition combatPosition in _selectionPriority)
            {
                if (Rebind(combatPosition)) return;
            }
        }
        
        public bool Rebind(CombatPosition combatPosition)
        {
            int index = _party.IndexOf(combatPosition);
            if (index == -1 || !_party.Get(index).Controllers.Get<CombatController>().IsAlive) return false;
            SelectedIndex = index;
            Rebind(_party.Get(index));
            return true;
        }

        public void Rebind(CombatActor actor)
        {
            float offsetY = _arrowOffsetY + actor.Controllers.Get<VisualController>().GetSize().Y/2;
            if (_arrowPosition == ArrowPosition.Above) offsetY *= -1;
            GlobalPosition = actor.GlobalPosition + new Vector2(0, offsetY);
        }
    }
}