using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection
{
    ///
    /// A <see cref="ICollection{T}"/> that is both synchronized and sticky.
    ///
    /// <para>Objects of this class are thread-safe.</para>
    ///
    public sealed class SolidCollection<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="array">source items</param>
        public SolidCollection(params T[] array) : this(new EnumerableOf<T>(array))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerator</param>
        public SolidCollection(IEnumerator<T> src) : this(new EnumerableOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public SolidCollection(IEnumerable<T> src) : this(new CollectionOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        public SolidCollection(ICollection<T> src) : base(
            () => 
                new SyncCollection<T>(
                    new StickyCollection<T>(src)))
        { }

    }
}
