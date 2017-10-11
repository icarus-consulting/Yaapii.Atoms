using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Lookup if an item is in a enumerable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ContainsEnumerator<T> : IScalar<bool>
        where T: IComparable<T>
    {
        private readonly T _item;
        private readonly IEnumerator<T> _src;

        /// <summary>
        /// Lookup the item in the src.
        /// </summary>
        /// <param name="src">src enumerable</param>
        /// <param name="item">lookup item</param>
        public ContainsEnumerator(IEnumerator<T> src, T item)
        {
            _item = item;
            _src = src;
        }

        /// <summary>
        /// Determine if the item is in the enumerable.
        /// </summary>
        /// <returns>true if item is present in enumerable.</returns>
        public bool Value()
        {
            new FailPrecise(
                    new FailWhen(!this._src.MoveNext()),
                    new IOException("cannot lookup in empty enumerable")).Go();

            var contains = true;
            for (var cur = 0; this._src.Current.CompareTo(_item) != 0; cur++)
            {
                if (!this._src.MoveNext())
                {
                    contains = false;
                    break;
                }
            }

            return contains;
        }
    }
}
