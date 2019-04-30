// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A sorted <see cref="ICollection{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Sorted<T> : CollectionEnvelope<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// A list with default sorting (ascending)
        /// </summary>
        /// <param name="src">the source enumerable</param>
        public Sorted(params T[] src) : this(new ListOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> with default sorting (ascending)
        /// </summary>
        /// <param name="src">the source enumerable</param>
        public Sorted(IEnumerable<T> src) : this(
            Comparer<T>.Default,
            new CollectionOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerable</param>
        public Sorted(Comparer<T> cmp, params T[] src) : this(cmp, new CollectionOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerator</param>
        public Sorted(Comparer<T> cmp, IEnumerator<T> src) : this(cmp, new CollectionOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerable</param>
        public Sorted(Comparer<T> cmp, IEnumerable<T> src) : this(cmp, new CollectionOf<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source collection</param>
        public Sorted(Comparer<T> cmp, ICollection<T> src) : base(
            () =>
                new CollectionOf<T>(
                    new Enumerator.Sorted<T>(cmp, src.GetEnumerator())))
        { }
    }
}
