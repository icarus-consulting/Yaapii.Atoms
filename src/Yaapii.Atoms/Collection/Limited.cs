using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A collection which is limited to a number of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Limited<T> : CollectionEnvelope<T>
    {

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">max number of items to limit to</param>
        /// <param name="src">items to limit</param>
        public Limited(int lmt, params T[] src) : this(lmt, new EnumerableOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">max number of items to limit to</param>
        /// <param name="src">Enumerator to limit</param>
        public Limited(int lmt, IEnumerator<T> src) : this(lmt, new EnumerableOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lmt">requested number of items</param>
        /// <param name="src">enumerable of items</param>
        public Limited(int lmt, IEnumerable<T> src) : this(lmt, new CollectionOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source collection</param>
        /// <param name="lmt">requested number of elements</param>
        public Limited(int lmt, ICollection<T> src) : base(
            () => new CollectionOf<T>(
                new Enumerable.Limited<T>(src, lmt)
            ))
        { }

    }
}
