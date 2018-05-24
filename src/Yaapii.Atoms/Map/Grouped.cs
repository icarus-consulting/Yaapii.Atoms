using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Groups a list to Keys and Lists of Values according to the given Functions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public sealed class Grouped<T, Key, Value> : MapEnvelope<Key, IList<Value>>
    {
        public Grouped(IEnumerable<T> list, IFunc<T, Key> keyFunc, IFunc<T, Value> valueFunc) : this(
            new ListOf<T>(list),
            keyFunc,
            valueFunc
        )
        { }

        public Grouped(IList<T> list, IFunc<T, Key> keyFunc, IFunc<T, Value> valueFunc) : base(
            () =>
            {
                Dictionary<Key, IList<Value>> temp = new Dictionary<Key, IList<Value>>();
                foreach(var entry in list)
                {
                    temp[keyFunc.Invoke(entry)] = new Mapped<T, Value>(valueFunc, list);
                }
                return temp;
            }
        )
        { }
    }
}
