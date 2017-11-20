using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// Envelope for collections. 
    /// It accepts a scalar and makes readonly Collection from it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CollectionOf<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Makes a collection from an array
        /// </summary>
        /// <param name="array"></param>
        public CollectionOf(params T[] array) : this(new EnumerableOf<T>(array))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public CollectionOf(IEnumerator<T> src) : this(new EnumerableOf<T>(src))
        { }

        /// <summary>
        /// Makes a collection from an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src"></param>
        public CollectionOf(IEnumerable<T> src) : base(
            () =>
            {
                ICollection<T> list = new LinkedList<T>();
                foreach (T item in src)
                {
                    list.Add(item);
                }
                return list;
            })
        { }
    }
}
