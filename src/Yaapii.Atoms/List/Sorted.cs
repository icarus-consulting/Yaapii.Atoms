using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Sorted<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private readonly IEnumerable<T> _enumerable;
        private readonly Comparer<T> _comparer;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(params T[] src) : this(Comparer<T>.Default, src)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IEnumerable<T> src) : this(Comparer<T>.Default, src)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public Sorted(Comparer<T> cmp, IEnumerable<T> src)
        {
            this._enumerable = src;
            this._comparer = cmp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SortedEnumerator<T>(this._comparer, this._enumerable.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics