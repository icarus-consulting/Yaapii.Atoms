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
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">Source Enumerable</param>
        /// <param name="keyFunc">Function to convert Source Type to Key Type</param>
        /// <param name="valueFunc">Function to Convert Source Type to Key Týpe</param>
        public Grouped(IEnumerable<T> src, IFunc<T, Key> keyFunc, IFunc<T, Value> valueFunc) : this(
            new ListOf<T>(src),
            keyFunc,
            valueFunc
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">Source Enumerable</param>
        /// <param name="keyFunc">Function to convert Source Type to Key Type</param>
        /// <param name="valueFunc">Function to Convert Source Type to Key Týpe</param>
        public Grouped(IList<T> src, IFunc<T, Key> keyFunc, IFunc<T, Value> valueFunc) : base(
            () =>
            {
                Dictionary<Key, IList<Value>> temp = new Dictionary<Key, IList<Value>>();
                foreach(var entry in src)
                {
                    temp[keyFunc.Invoke(entry)] = new Mapped<T, Value>(valueFunc, src);
                }
                return temp;
            }
        )
        { }
    }
}
