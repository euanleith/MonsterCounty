using System;
using System.Linq;
using Godot;
using Godot.Collections;

namespace MonsterCounty.UI
{
    public abstract partial class StaticContainer<[MustBeVariant] T> : Container where T : CanvasItem
    {
        protected Array<T> Array;
        private Action<T, int> _rebindElement;

        public void Load(Action<T, int> loadElement, Action<T, int> rebindElement)
        {
            _rebindElement = rebindElement;
            Array = new(GetChildren().OfType<T>());
            for (int i = 0; i < Array.Count; i++)
            {
                int capturedIndex = i;
                loadElement.Invoke(Array[i], capturedIndex);
            }
        }

        public void Rebind()
        {
            for (int i = 0; i < Array.Count; i++)
            {
                _rebindElement.Invoke(Array[i], i);
            }
        }
    }
}