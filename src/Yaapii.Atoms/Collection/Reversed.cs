using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection
{
    ///
    /// Reversed collection.
    ///
    /// <para>Pay attention that sorting will happen on each operation
    /// with the collection. Every time you touch it, it will fetch the
    /// entire collection from the encapsulated object and reverse it. If you
    /// want to avoid that "side-effect", decorate it with
    /// <see cref="StickyCollection{T}"/></para>
    ///
    /// <para>There is no thread-safety guarantee.</para>
    ///
    public class Reversed<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src"></param>
        public Reversed(params T[] src) : this(new EnumerableOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        public Reversed(IEnumerable<T> src) : this(new CollectionOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        public Reversed(ICollection<T> src) : base(
            () => new CollectionOf<T>(
                    new LinkedList<T>(src).Reverse()))
        { }
    }
}
