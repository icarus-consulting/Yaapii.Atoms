using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// Joins collections together as one.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Joined<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list"></param>
        public Joined(params T[] list) : this(
            new CollectionOf<T[]>(list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of collections to join together</param>
        public Joined(IEnumerable<IEnumerable<T>> list) : base(
            () => new CollectionOf<T>(
                    new Enumerable.Joined<T>(list)
             ))
        { }

    }
}
