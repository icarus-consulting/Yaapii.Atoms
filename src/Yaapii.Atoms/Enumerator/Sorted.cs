// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Scalar;

#pragma warning disable NoProperties // No Properties
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> sorted by the given <see cref="Comparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of items in enumertor</typeparam>
    public sealed class Sorted<T> : IEnumerator<T>
        where T : IComparable<T>
    {
        private readonly IScalar<IEnumerator<T>> sorted;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> sorted by the given <see cref="Comparer{T}"/>.
        /// </summary>
        /// <param name="cmp">comparer</param>
        /// <param name="src">enumerator to sort</param>
        public Sorted(Comparer<T> cmp, IEnumerator<T> src)
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
                    items.Sort(cmp);

                    return items.GetEnumerator();
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
}
#pragma warning restore NoProperties // No Properties
