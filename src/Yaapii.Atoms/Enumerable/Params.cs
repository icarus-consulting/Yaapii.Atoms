using System;
using System.Collections;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Enumeration from array of items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Params<T> : System.Collections.Generic.IEnumerable<T>
    {
        private readonly T[] items;

        /// <summary>
        /// Enumeration from array of items.
        /// </summary>
        /// <param name="items"></param>
        public Params(params T[] items)
        {
            this.items = items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var item in this.items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

