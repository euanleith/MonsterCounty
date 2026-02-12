using System;
using System.Collections.Generic;

namespace MonsterCounty.Model
{
    public class TypeMap<TBase> where TBase : class
    {
        private Dictionary<Type, TBase> map = new();

        public void Add<TDerived>(TDerived obj) where TDerived : TBase
        {
            map[typeof(TDerived)] = obj;
        }

        public TDerived Get<TDerived>() where TDerived : class, TBase
        {
            return map.TryGetValue(typeof(TDerived), out var value)
                ? (TDerived)value
                : null;
        }
        
        public void ForEach(Action<TBase> action)
        {
            foreach (var value in map.Values)
                action(value);
        }
    }
}