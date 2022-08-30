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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> sorted by the given <see cref="Comparer{TKey}"/>.
    /// </summary>
    /// <typeparam name="T">type of items in enumerator</typeparam>
    public sealed class SortedBy<T, TKey> : IEnumerator<T>
        where TKey : IComparable<TKey>
    {
        private readonly IScalar<IEnumerator<T>> sorted;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> sorted by the given <see cref="Comparer{TKey}"/>.
        /// </summary>
        /// <param name="swap">func to swap the type to a sortable type</param>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerator to sort</param>
        public SortedBy(Func<T, TKey> swap, IComparer<TKey> cmp, IEnumerator<T> src)
        {
            this.sorted =
                new ScalarOf<IEnumerator<T>>(
                () =>
                {
                    var items = new List<T>();
                    while (src.MoveNext())
                    {
                        items.Add(src.Current);
                    }

                    return items.OrderBy(swap, cmp).GetEnumerator();
                }
                );
        }

        public void Dispose()
        { }

        public Boolean MoveNext()
        {
            return this.sorted.Value().MoveNext();
        }

        public T Current
        {
            get
            {
                return this.sorted.Value().Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.sorted.Value().Current;
            }
        }

        public void Reset()
        {
            throw new NotSupportedException("#Reset() is not supported");
        }
    }

    /// <summary>
    /// A <see cref="IEnumerator{T}"/> sorted by the given <see cref="Comparer{TKey}"/>.
    /// </summary>
    /// <typeparam name="T">type of items in enumerator</typeparam>
    public static class SortedBy
    {
        /// <summary>
        /// A <see cref="IEnumerator{T}"/> sorted by the given <see cref="Comparer{TKey}"/>.
        /// </summary>
        /// <typeparam name="T">type of items in enumerator</typeparam>
        public static IEnumerator<T> New<T, TKey>(Func<T, TKey> swap, IComparer<TKey> cmp, IEnumerator<T> src)
            where TKey : IComparable<TKey>
            => new SortedBy<T, TKey>(swap, cmp, src);
    }

}
