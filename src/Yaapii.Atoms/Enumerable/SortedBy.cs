// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class SortedBy<T, TKey> : ManyEnvelope<T>
        where TKey : IComparable<TKey>
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public SortedBy(Func<T, TKey> swap, params T[] src) : this(
            swap,
            Comparer<TKey>.Default,
            src
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public SortedBy(Func<T, TKey> swap, IEnumerable<T> src) : this(
            swap,
            Comparer<TKey>.Default,
            src
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public SortedBy(Func<T, TKey> swap, Comparison<TKey> compare, IEnumerable<T> src) : this(
            swap,
            Comparer<TKey>.Create(compare),
            src
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public SortedBy(Func<T, TKey> swap, Comparer<TKey> cmp, IEnumerable<T> src) : base(() =>
            new LiveMany<T>(() =>
                new Enumerator.SortedBy<T, TKey>(swap, cmp, src.GetEnumerator())
            ),
            false
        )
        { }
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public static class SortedBy
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T, TKey>(Func<T, TKey> swap, params T[] src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, src);


        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T, TKey>(Func<T, TKey> swap, IEnumerable<T> src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T, TKey>(Func<T, TKey> swap, Comparer<TKey> cmp, IEnumerable<T> src) where TKey : IComparable<TKey> =>
            new SortedBy<T, TKey>(swap, cmp, src);
    }
}

