using System;
using System.Reflection;
using Yaapii.Atoms.Text;
using System.Collections.Generic;
using System.Collections;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A collection which is threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SyncCollection<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SyncCollection() : this(new object())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot"></param>
        public SyncCollection(object syncRoot) : this(syncRoot, new CollectionOf<T>())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">items to make collection from</param>
        public SyncCollection(params T[] items) : this(new CollectionOf<T>(items))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="col">Collection to sync</param>
        public SyncCollection(ICollection<T> col) : this(col, col)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot">root object to sync</param>
        /// <param name="col"></param>
        public SyncCollection(object syncRoot, ICollection<T> col) : base(
            new SyncScalar<ICollection<T>>(
                new ScalarOf<ICollection<T>>(() =>
                {
                    lock (syncRoot)
                    {
                        var tmp = new List<T>();
                        foreach (var item in col)
                        {
                            tmp.Add(item);
                        }
                        return tmp;
                    }
                }
        )))
        { }
    }
}