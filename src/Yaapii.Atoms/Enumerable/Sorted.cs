// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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
using System.Collections;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given comparison .
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Sorted<T> : IEnumerable<T>
    {
        private readonly List<T> source;
        private readonly bool[] sorted;
        private readonly IComparer<T> comparer;
        private readonly bool live;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(params T[] src) : this(Comparer<T>.Default, src)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IEnumerable<T> src, bool live = false) : this(Comparer<T>.Default, src, live)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(Func<T, T, int> comparison, params T[] src) : this(
            Comparer<T>.Create(
                new Comparison<T>((left, right) => comparison(left, right))
            ),
            src,
            false
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(Func<T, T, int> comparison, bool live, params T[] src) : this(
            Comparer<T>.Create(
                new Comparison<T>((left, right) => comparison(left, right))
            ),
            src,
            live
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public Sorted(Func<T, T, int> comparison, IEnumerable<T> src, bool live = false) : this(
            Comparer<T>.Create(
                new Comparison<T>((left, right) => comparison(left, right))
            ),
            src,
            live
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public Sorted(IComparer<T> cmp, IEnumerable<T> src, bool live = false)
        {
            this.source = new List<T>(src);
            this.sorted = new bool[] { false };
            this.comparer = cmp;
            this.live = live;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(!this.IsSorted() || this.live)
            {
                this.Sort();
            }
            foreach(var item in this.source)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool IsSorted()
        {
            return this.sorted[0];
        }

        private void Sort()
        {
            this.source.Sort(this.comparer);
            this.sorted[0] = true;
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public static class Sorted
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> with the given items sorted by default.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(params T[] src) =>
            new Sorted<T>(src);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> src, bool live = false) =>
            new Sorted<T>(src, live);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(IComparer<T> cmp, IEnumerable<T> src, bool live = false) =>
            new Sorted<T>(cmp, src, live);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="comparison">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(Func<T, T, int> comparison, params T[] src) =>
            new Sorted<T>(comparison, src, false);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="comparison">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(Func<T, T, int> comparison, bool live, params T[] src) =>
            new Sorted<T>(comparison, src, live);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="comparison">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(Func<T, T, int> comparison, IEnumerable<T> src) =>
            new Sorted<T>(comparison, src, false);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="comparison">comparer</param>
        /// <param name="src">enumerable to sort</param>
        public static IEnumerable<T> New<T>(Func<T, T, int> comparison, IEnumerable<T> src, bool live) =>
            new Sorted<T>(comparison, src, live);
    }
}

