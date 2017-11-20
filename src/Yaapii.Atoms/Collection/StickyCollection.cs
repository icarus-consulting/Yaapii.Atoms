using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// Makes a collection that iterates only once.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StickyCollection<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Makes a collection of given items.
        /// </summary>
        /// <param name="items">source items</param>
        public StickyCollection(params T[] items) : this(new EnumerableOf<T>(items))
        { }

        /// <summary>
        /// Makes a collection of given items.
        /// </summary>
        /// <param name="items">source items</param>
        public StickyCollection(IEnumerator<T> items) : this(new EnumerableOf<T>(items))
        { }

        /// <summary>
        /// Makes a collection of given items.
        /// </summary>
        /// <param name="items">source items</param>
        public StickyCollection(IEnumerable<T> items) : this(new CollectionOf<T>(items))
        { }

        /// <summary>
        /// Makes a collection of given items.
        /// </summary>
        /// <param name="list">list of source items</param>
        public StickyCollection(ICollection<T> list) : base(
                new StickyScalar<ICollection<T>>( //Make a sticky scalar which copies the items once and returns them always.
                    () =>
                    {
                        var temp = new List<T>(list.Count);
                        foreach(var item in list)
                        {
                            temp.Add(item);
                        }
                        return new CollectionOf<T>(temp);
                    }
            ))
        { }
    }
}
