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
using System.Collections;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Multiple enumerables merged together, so that every entry is unique.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Distinct<T> : System.Collections.Generic.IEnumerable<T>
    {
        private readonly System.Collections.Generic.IEnumerable<T> all;
        private readonly Comparison<T> comparison;
        private readonly System.Collections.Generic.IEnumerable<T> result;

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public Distinct(params System.Collections.Generic.IEnumerable<T>[] enumerables) : this(
            new LiveMany<System.Collections.Generic.IEnumerable<T>>(enumerables)
        )
        { }

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public Distinct(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> enumerables, bool live = false) : this(
            enumerables,
            (v1, v2) => v1.Equals(v2),
            live
        )
        { }

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        /// <param name="comparison">comparison to evaluate distinction</param>
        public Distinct(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> enumerables, Func<T, T, bool> comparison, bool live = false)
        {
            this.all = new Joined<T>(enumerables);
            this.comparison = new Comparison<T>(comparison);
            this.result =
                Ternary.New(
                    LiveMany.New(Produced),
                    Sticky.New(Produced),
                    live
                );
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private System.Collections.Generic.IEnumerable<T> Produced()
        {
            var set = new HashSet<T>(this.comparison);
            foreach (var item in this.all)
            {
                if (set.Add(item))
                    yield return item;
            }
        }

        private sealed class Comparison<TItem> : IEqualityComparer<T>
        {
            private readonly Func<T, T, bool> comparison;

            public Comparison(Func<T, T, bool> comparison)
            {
                this.comparison = comparison;
            }

            public bool Equals(T x, T y)
            {
                return this.comparison.Invoke(x, y);
            }

            public int GetHashCode(T obj)
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// Multiple enumerables merged together, so that every entry is unique.
    /// </summary>
    public static class Distinct
    {
        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public static System.Collections.Generic.IEnumerable<T> New<T>(params System.Collections.Generic.IEnumerable<T>[] enumerables) => new Distinct<T>(enumerables);

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public static System.Collections.Generic.IEnumerable<T> New<T>(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> enumerables) => new Distinct<T>(enumerables);

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        /// <param name="comparison">comparison to evaluate distinction</param>
        public static System.Collections.Generic.IEnumerable<T> New<T>(System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> enumerables, Func<T, T, bool> comparison) => new Distinct<T>(enumerables, comparison);
    }


}