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

using System.Collections.Generic;
using Yaapii.Atoms.Func;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> joined together.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Joined<T> : ManyEnvelope<T>
    {
        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(T first, T second, IEnumerable<T> lst, params T[] items) : base(() =>
        {
            int idx = 2;
            T[] joined = new T[1 + new LengthOf(lst).Value() + items.Length];
            joined[0] = first;
            joined[1] = second;
            var enm = lst.GetEnumerator();
            while (enm.MoveNext())
            {
                joined[idx] = enm.Current;
                idx++;
            }

            foreach (var item in items)
            {
                joined[idx] = item;
                idx++;
            }
            return new ManyOf<T>(joined);
        },
            false
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(T first, IEnumerable<T> lst, params T[] items) : base(() =>
            {
                int idx = 1;
                T[] joined = new T[1 + new LengthOf(lst).Value() + items.Length];
                joined[0] = first;
                var enm = lst.GetEnumerator();
                while (enm.MoveNext())
                {
                    joined[idx] = enm.Current;
                    idx++;
                }

                foreach (var item in items)
                {
                    joined[idx] = item;
                    idx++;
                }
                return new ManyOf<T>(joined);
            },
            false
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(IEnumerable<T> lst, params T[] items) : base(() =>
            {
                int idx = 0;
                T[] joined = new T[new LengthOf(lst).Value() + items.Length];
                var enm = lst.GetEnumerator();
                while (enm.MoveNext())
                {
                    joined[idx] = enm.Current;
                    idx++;
                }

                foreach (var item in items)
                {
                    joined[idx] = item;
                    idx++;
                }
                return new ManyOf<T>(joined);
            },
            false
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(params IEnumerable<T>[] items) : this(
            new ManyOf<IEnumerable<T>>(items)
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(IEnumerable<IEnumerable<T>> items) : base(() =>
            new LiveMany<T>(() =>
                new Enumerator.Joined<T>(
                    new Mapped<IEnumerable<T>, IEnumerator<T>>(//Map the content of list: Get every enumerator out of it and build one whole enumerator from it
                        new StickyFunc<IEnumerable<T>, IEnumerator<T>>( //Sticky Gate
                            new FuncOf<IEnumerable<T>, IEnumerator<T>>(
                                e => e.GetEnumerator() //Get the Enumerator
                            )
                        ),
                        items //List with enumerators
                    )
                )
            ),
            false
        )
        { }
    }

    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> joined together.
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
        /// Multiple <see cref="IEnumerable{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> New<T>(params IEnumerable<T>[] items) => new Joined<T>(items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> New<T>(IEnumerable<IEnumerable<T>> items) => new Joined<T>(items);
    }
}
