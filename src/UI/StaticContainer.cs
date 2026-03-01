using System;
using System.Linq;
using Godot;
using Godot.Collections;

namespace MonsterCounty.UI
{
    public abstract partial class StaticContainer<[MustBeVariant] T> : Container where T : CanvasItem
    {
        private Array<T> _array;
        private Action<T, int> _rebindElement;

        public void Load(Action<T, int> loadElement, Action<T, int> rebindElement)
        {
            _rebindElement = rebindElement;
            _array = new(GetChildren().OfType<T>());
            for (int i = 0; i < _array.Count; i++)
            {
                int capturedIndex = i;
                loadElement.Invoke(_array[i], capturedIndex);
            }
        }

        public void Rebind()
        {
            for (int i = 0; i < _array.Count; i++)
            {
                _rebindElement.Invoke(_array[i], i);
            }
        }
    }
}