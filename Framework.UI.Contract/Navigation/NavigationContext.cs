using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Contract.Navigation
{
    public class NavigationContext
    {
        public NavigationContext()
        {
            Params = new List<KeyValuePair<Type, object>>();
        }
        public List<KeyValuePair<Type, object>> Params;

        public T OfType<T>()
        {
            return (T) Params.First(p => p.Key == typeof(T)).Value;
        }

        public T[] AllOfType<T>()
        {
            return  Params.Where(p => p.Key == typeof(T)).Select(p => (T) p.Value).ToArray();
        }
    }
}