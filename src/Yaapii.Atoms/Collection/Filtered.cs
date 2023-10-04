// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A filtered <see cref="ICollection{T}"/>
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
                new LiveMany<T>(() =>
                    new Enumerable.Joined<T>(
                        new ManyOf<T>(
                            item1,
                            item2
                        ),
                        items
                    ).GetEnumerator()
                )
            )
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> filtered by the given <see cref="Func{T, TResult}"/>
        /// </summary>
        /// <param name="func">filter func</param>
        /// <param name="src">items to filter</param>
        public Filtered(Func<T, Boolean> func, IEnumerator<T> src) : this(func, new ManyOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> filtered by the given <see cref="Func{T, TResult}"/>
        /// </summary>
        /// <param name="func">filter func</param>
        /// <param name="src">items to filter</param>
        public Filtered(Func<T, Boolean> func, IEnumerable<T> src) : base(
            new Live<ICollection<T>>(() =>
                new LiveCollection<T>(
                    new Enumerable.Filtered<T>(
                        func, src
                    )
                )
            ),
            false
        )
        { }
        

    }

    public static class Filtered
    {
        public static ICollection<T> New<T>(Func<T, Boolean> func, IEnumerable<T> src) => new Filtered<T>(func, src);
        public static ICollection<T> New<T>(Func<T, Boolean> func, IEnumerator<T> src) => new Filtered<T>(func, src);
        public static ICollection<T> New<T>(Func<T, Boolean> func, T item1, T item2, params T[] items) => new Filtered<T>(func, item1, item2, items);
    }
}
