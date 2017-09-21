using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="X">type of items in enumerable</typeparam>
    public sealed class Reversed<X> : IEnumerable<X>
    {
        private readonly IEnumerable<X> _enumerable;

        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public Reversed(IEnumerable<X> src)
        {
            this._enumerable = src;
        }

        public IEnumerator<X> GetEnumerator()
        {
            List<X> list = new List<X>(this._enumerable);
            list.Reverse();
            return list.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics
