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
        public Sorted(params T[] src) : this(new LiveMany<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> with default sorting (ascending)
        /// </summary>
        /// <param name="src">the source enumerable</param>
        public Sorted(IEnumerable<T> src) : this(
            Comparer<T>.Default,
            new LiveCollection<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerable</param>
        public Sorted(Comparer<T> cmp, params T[] src) : this(cmp, new LiveCollection<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerator</param>
        public Sorted(Comparer<T> cmp, IEnumerator<T> src) : this(cmp, new LiveCollection<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source enumerable</param>
        public Sorted(Comparer<T> cmp, IEnumerable<T> src) : this(cmp, new LiveCollection<T>(src))
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> sorted using the given <see cref="Comparer{T}"/>
        /// </summary>
        /// <param name="cmp">the comparer</param>
        /// <param name="src">the source collection</param>
        public Sorted(Comparer<T> cmp, ICollection<T> src) : base(
            () =>
                new LiveCollection<T>(
                    new Enumerable.Sorted<T>(cmp, src)
                ),
            false
        )
        { }
    }

    public static class Sorted
    {
        /// <summary>
        public static ICollection<T> New<T>(params T[] src) where T : IComparable<T> => new Sorted<T>(src);

        public static ICollection<T> New<T>(IEnumerable<T> src) where T : IComparable<T> => new Sorted<T>(src);

        public static ICollection<T> New<T>(Comparer<T> cmp, params T[] src) where T : IComparable<T> => new Sorted<T>(cmp, src);

        public static ICollection<T> New<T>(Comparer<T> cmp, IEnumerator<T> src) where T : IComparable<T> => new Sorted<T>(cmp, src);

        public static ICollection<T> New<T>(Comparer<T> cmp, ICollection<T> src) where T : IComparable<T> => new Sorted<T>(cmp, src);
    }
}
