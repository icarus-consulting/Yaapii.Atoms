﻿using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A filtered <see cref="ICollection{T}"/>.
    /// Pass a filter function which will applied to all items, similar to List{T}.Where(...) in LinQ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Filtered<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// A filtered <see cref="ICollection{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="func">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public Filtered(Func<T, Boolean> func, T item1, T item2, params T[] items) :
            this(
                func,
                new EnumerableOf<T>(
                    new ScalarOf<IEnumerator<T>>(
                        () => new Joined<T>(
                            new EnumerableOf<T>(
                                item1,
                                item2),
                            items).GetEnumerator())))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">filter function</param>
        /// <param name="src">Source collection</param>
        public Filtered(Func<T, Boolean> func, IEnumerator<T> src) : this(func, new EnumerableOf<T>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">filter function</param>
        /// <param name="src">Source collection</param>
        public Filtered(Func<T, Boolean> func, IEnumerable<T> src) : base(new ScalarOf<ICollection<T>>(() => new CollectionOf<T>(
                 new Enumerable.Filtered<T>(
                     func, src
                 ))))
        { }

    }
}