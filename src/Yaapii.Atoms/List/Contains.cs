using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Lookup if an item is in a enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Contains<T> : IScalar<bool>
        where T: IComparable<T>
    {
        private readonly T _item;
        private readonly IEnumerable<T> _src;

        /// <summary>
        /// Lookup if an item is in a enumerable.
        /// </summary>
        /// <param name="item">item to lookup</param>
        /// <param name="src">enumerable to test</param>
        public Contains(T item, IEnumerable<T> src)
        {
            _src = src;
            _item = item;
        }

        /// <summary>
        /// see if the item is in the enumerable.
        /// </summary>
        /// <returns>true if item is in the enumerable</returns>
        public bool Value()
        {
            return new ContainsEnumerator<T>(_src.GetEnumerator(), _item).Value();
        }
    }
}
