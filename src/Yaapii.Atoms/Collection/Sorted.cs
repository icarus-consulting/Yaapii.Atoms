using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A sorted <see cref="ICollection{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Sorted<T> : CollectionEnvelope<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// A list with default sorting (ascending)
        /// </summary>
        /// <param name="src">the source enumerable</param>
        public Sorted(params T[] src) : this(new ListOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> with default sorting (ascending)
        /// </summary>
        /// <param name="src">the source enumerable</param>
        public Sorted(IEnumerable<T> src) : this(
            Comparer<T>.Default,
            new CollectionOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerable</param>
        public Sorted(Comparer<T> cmp, params T[] src) : this(cmp, new CollectionOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerator</param>
        public Sorted(Comparer<T> cmp, IEnumerator<T> src) : this(cmp, new CollectionOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerable</param>
        public Sorted(Comparer<T> cmp, IEnumerable<T> src) : this(cmp, new CollectionOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source collection</param>
        public Sorted(Comparer<T> cmp, ICollection<T> src) : base(
            () =>
                new CollectionOf<T>(
                    new SortedEnumerator<T>(cmp, src.GetEnumerator())))
        { }
    }
}
