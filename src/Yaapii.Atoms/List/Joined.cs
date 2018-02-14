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
    /// Multiple lists joined together as one.
    /// </summary>
    /// <typeparam name="T">type of items in list</typeparam>
    public sealed class Joined<T> : ListEnvelope<T>
    {
        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="origin">a list to join</param>
        /// <param name="src">lists to join</param>
        public Joined(IList<T> origin, params IList<T>[] src) : this(
            new Enumerable.Joined<IList<T>>(
                new EnumerableOf<IList<T>>(origin), src))
        { }

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="src">The lists to join together</param>
        /// <param name="origin">a list to join</param>
        public Joined(IList<T> origin, params T[] src) : this(
            new Enumerable.Joined<IList<T>>(
                new EnumerableOf<IList<T>>(origin), src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(params IList<T>[] src) : this(new EnumerableOf<IList<T>>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(IEnumerable<IList<T>> src) : base(() =>
            {
                var blocking = new BlockingCollection<T>();
                foreach (var lst in src)
                {
                    new And<T>(item => { blocking.Add(item); return true; }, lst).Value();
                }

                return new ListOf<T>(blocking);
            })
        { }
    }
}
