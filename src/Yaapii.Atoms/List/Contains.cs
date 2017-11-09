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
        where T : IComparable<T>
    {
        private readonly IEnumerable<T> _items;
        private readonly Func<T, bool> _match;

        /// <summary>
        /// Lookup if an item is in a enumerable by calling .Equals(...) of the item.
        /// </summary>
        /// <param name="item">item to lookup</param>
        /// <param name="src">enumerable to test</param>
        public Contains(IEnumerable<T> src, T item) : this(
            src,
            (cdd) => cdd.Equals(item))
        { }

        /// <summary>
        /// Lookup if any item matches the given function
        /// </summary>
        /// <param name="items">enumerable to search through</param>
        /// <param name="match">check to perform on each item</param>
        public Contains(IEnumerable<T> src, Func<T, bool> match)
        {
            _match = match;
            _items = src;
        }

        /// <summary>
        /// see if the item is in the enumerable.
        /// </summary>
        /// <returns>true if item is in the enumerable</returns>
        public bool Value()
        {
            return new ContainsEnumerator<T>(_items.GetEnumerator(), match).Value();
        }
    }
}
