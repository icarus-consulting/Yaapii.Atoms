using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A list which is threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SyncList<T> : ListEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">source items</param>
        public SyncList(params T[] items) : this(new EnumerableOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">source items</param>
        public SyncList(IEnumerable<T> items) : this(new ListOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">source item enumerator</param>
        public SyncList(IEnumerator<T> items) : this(new ListOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">A threadsafe list</param>
        public SyncList(ICollection<T> list) : base(
                new SyncScalar<IList<T>>(
                    new ScalarOf<IList<T>>(() =>
                        new ListOf<T>(
                            new SyncCollection<T>(list)))))
        { }
    }
}
