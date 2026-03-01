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
            Rebind(0);
        }
        
        private void Rebind(int index)
        {
            for (; index < _party.Count(); index++)
            {
                if (_party.Get(index).Controllers.Get<CombatController>().IsAlive)
                {
                    SelectedIndex = index;
                    Rebind(_party.Get(index));
                    break;
                }
            }
        }

        public void Rebind(CombatActor actor)
        {
            float offsetY = _arrowOffsetY + actor.Controllers.Get<VisualController>().GetSize().Y/2;
            if (_arrowPosition == ArrowPosition.Above) offsetY *= -1;
            GlobalPosition = actor.GlobalPosition + new Vector2(0, offsetY);
        }

        public void Next()
        {
            for (var index = SelectedIndex+1; index < _party.Count(); index++)
            {
                if (_party.Get(index).Controllers.Get<CombatController>().IsAlive)
                {
                    Rebind(index);
                }
            }
        }

        public void Prev()
        {
            for (var index = SelectedIndex-1; index >= 0; index--)
            {
                if (_party.Get(index).Controllers.Get<CombatController>().IsAlive)
                {
                    Rebind(index);
                }
            }
        }
    }
}