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

using System.Collections;
using System.Collections.Generic;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Joined<T> : IEnumerable<T>
    {
        private readonly IEnumerable<IEnumerable<T>> enumerables;
        private readonly Ternary<T> result;

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(T first, T second, IEnumerable<T> lst, bool live = false, params T[] items) : this(
            live,
            new Single<T>(first),
            new Single<T>(second),
            lst,
            new Params<T>(items)
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(T first, IEnumerable<T> lst, bool live = false, params T[] items) : this(
            new Params<IEnumerable<T>>(
                new Single<T>(first),
                lst,
                new Params<T>(items)
            ),
            live
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(bool live, IEnumerable<T> lst, params T[] items) : this(
            lst, live, items
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(IEnumerable<T> lst, params T[] items) : this(
            lst, false, items
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(IEnumerable<T> lst, bool live = false, params T[] items) : this(
            new Params<IEnumerable<T>>(lst, new Params<T>(items)), live
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(params IEnumerable<T>[] items) : this(
            false, items
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(bool live = false, params IEnumerable<T>[] items) : this(
            new Params<IEnumerable<T>>(items), live
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(IEnumerable<IEnumerable<T>> items, bool live = false)
        {
            this.enumerables = items;
            this.result =
                Ternary.New(
                    LiveMany.New(Produced),
                    Sticky.New(Produced),
                    live
                );

        }

        public IEnumerator<T> GetEnumerator()
        {
            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Produced()
        {
            foreach (var enumerable in this.enumerables)
            {
                foreach (var item in enumerable)
                {
                    yield return item;
                }
            }
        }
    }

    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
    /// </summary>
    public static class Joined
    {
        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> lst, params T[] items) => new Joined<T>(lst, items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> New<T>(params IEnumerable<T>[] items) => new Joined<T>(items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> New<T>(IEnumerable<IEnumerable<T>> items) => new Joined<T>(items);
    }
}
