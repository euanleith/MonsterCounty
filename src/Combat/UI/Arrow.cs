using System;
using Godot;
using Godot.Collections;
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
        
        public int SelectedIndex;
        private int _maxIndex;
        private Array<CombatActor> _party;
        private ArrowPosition _arrowPosition;
        private float _arrowOffsetY;

        // todo how to generalise this wrt static/dynamic uielement?
        public void Load(ArrowPosition arrowPosition, float arrowOffsetY)
        {
            _arrowPosition = arrowPosition;
        }

        public void Bind(Array<CombatActor> party, int maxIndex)
        {
            _party = party;
            _maxIndex = maxIndex;
            Rebind(0);
        }
        
        private void Rebind(int index)
        {
            SelectedIndex = index;
            Rebind(_party[index]);
        }

        public void Rebind(CombatActor actor)
        {
            float offsetY = _arrowOffsetY + actor.Controllers.Get<VisualController>().GetSize().Y/2;
            if (_arrowPosition == ArrowPosition.Above) offsetY *= -1;
            GlobalPosition = actor.GlobalPosition + new Vector2(0, offsetY);
        }

        public void Next()
        {
            Rebind(Math.Min(SelectedIndex+1, _maxIndex));
        }

        public void Prev()
        {
            Rebind(Math.Max(SelectedIndex-1, 0));
        }
    }
}