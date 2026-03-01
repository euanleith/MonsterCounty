using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace MonsterCounty.UI
{
    public abstract partial class DynamicContainer<[MustBeVariant] T, U> : Container where T : CanvasItem
    {
        private T _template;
        private Action<T, U> _bindElement;

        public void Load(Action<T, U> bindElement)
        {
            _bindElement = bindElement;
            _template = GetChildren().OfType<T>().First();
        }

        public void LoadAndBind(Action<T, U> bindElement, IEnumerable<U> enumerable)
        {
            Load(bindElement);
            Bind(enumerable);
        }
        
        public void Bind(IEnumerable<U> enumerable)
        {
            GetChildren()
                .OfType<T>()
                .Where(child => child != _template)
                .ToList()
                .ForEach(RemoveChild);
            foreach (U elem in enumerable)
            {
                T obj = _template.Duplicate() as T;
                obj.Visible = true;
                _bindElement?.Invoke(obj, elem);
                AddChild(obj);
            }
            _template.Visible = false;
        }
    }
}