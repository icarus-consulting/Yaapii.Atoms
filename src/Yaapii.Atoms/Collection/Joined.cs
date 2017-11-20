using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;

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
        /// <param name="list">List of collections to join together</param>
        public Joined(params IEnumerable<T>[] list) : base(
            () => new CollectionOf<T>(
                    new Enumerable.Joined<T>(list)
             ))
        { }

    }
}
