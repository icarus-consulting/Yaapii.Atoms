// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Find the smallest item in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Min<T> : IScalar<T>
        where T : IComparable<T>
    {
        private readonly IEnumerable<IScalar<T>> items;

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items"><see cref="Func{TResult}"/> functions which retrieve items to compare</param>
        public Min(params Func<T>[] items) : this(
            new Enumerable.Mapped<Func<T>, IScalar<T>>(
                item => new ScalarOf<T>(() => item.Invoke()),
                new EnumerableOf<Func<T>>(items)))
        { }

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(IEnumerable<T> items) : this(
            new Enumerable.Mapped<T, IScalar<T>>(
                item => new ScalarOf<T>(item),
                items))
        { }

        /// <summary>
        /// Find the smallest item in the given items
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(params T[] items) : this(
            new Enumerable.Mapped<T, IScalar<T>>(
                item => new ScalarOf<T>(item),
                items))
        { }

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(IEnumerable<IScalar<T>> items)
        {
            this.items = items; //C# params Bug
        }

        /// <summary>
        /// Find the smallest item in the given scalars.
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(params IScalar<T>[] items)
        {
            this.items = items;
        }

        /// <summary>
        /// Get the minimum.
        /// </summary>
        /// <returns>the minimum</returns>
        public T Value()
        {
            IEnumerator<IScalar<T>> e = this.items.GetEnumerator();
            if (!e.MoveNext())
            {
                throw new NoSuchElementException("Can't find greater element in an empty iterable");
            }

            T min = e.Current.Value();
            while (e.MoveNext())
            {
                T next = e.Current.Value();
                if (next.CompareTo(min) < 0)
                {
                    min = next;
                }
            }
            return min;
        }

    }
}
